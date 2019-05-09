using AutoMapper;
using SonarBrowser.ActiveDirectory.Service;
using SonarBrowser.Services.DTO;
using SonarBrowser.Sonar.Service;
using SonarBrowser.Sonar.Service.Model;
using SonarBrowser.Sonar.Services.Model.DTO;
using SonarBrowser.SonarBrowserOrchestrator.Services.DTO;
using SonarBrowser.Tfs.Service.Interface;
using System.Collections.Generic;

namespace SonarBrowser.SonarBrowserOrchestrator.Services.Assembler
{
    internal static class IssueSonarAssembler
    {
        /// <summary>
        /// The internal Assembler converting the <see cref="SearchIssuesResponse"/> to an enriched list of Sonar Issues.
        /// </summary>
        /// <param name="searchIssuesResponse">The list of Issues from the Sonar API.</param>
        /// <param name="groupADSet">The active directory information needed to get some informations.</param>
        /// <param name="mapper">The AutoMapper interface to create the Issues Detail <see cref="IssueDetail"/>.</param>
        /// <param name="sonarConnector">The link to the Sonar Services.</param>
        /// <returns>An enriched and filtered list of Sonar Issues.</returns>
        internal static List<Issue> CreateIssueSet(this SearchIssuesResponse searchIssuesResponse, GroupADSet groupADSet, IMapper mapper, ISonarConnector sonarConnector, ITfsConnector tfsConnector)
        {
            List<Issue> issueSet = new List<Issue>();
            if (searchIssuesResponse?.issues == null)
            {
                return issueSet;
            }

            issueSet.Capacity = searchIssuesResponse.issues.Count;
            foreach (var responseIssue in searchIssuesResponse.issues)
            {
                if (responseIssue != null)
                {
                    Issue issue = new Issue()
                    {
                        IssueDetail = mapper.Map<IssueDetail>(responseIssue)
                    };

                    issue = issue.AddChangeSet(sonarConnector);
                    if (issue.ChangeSetDate != null && issue.ChangeSetDate?.AddDays(5) > issue.IssueDetail.creationDate)
                    {
                        issue = issue.AddCodeProject(tfsConnector);
                        issue = issue.AddActiveDirectoryGroup(groupADSet);
                        issue = issue.AddCodeLineCount(tfsConnector);

                        issueSet.Add(issue);
                    }
                }
            }

            issueSet.TrimExcess();
            return issueSet;
        }

        /// <summary>
        /// Enrich the issue with it's corresponding Active Directory group.
        /// </summary>
        /// <param name="issue">The current issue.</param>
        /// <param name="groupADSet">The list of AD groups.</param>
        /// <returns>The issue containing it's ADGroup.</returns>
        private static Issue AddActiveDirectoryGroup(this Issue issue, GroupADSet groupADSet)
        {
            Issue returnIssue = issue;

            returnIssue.ADGroup = groupADSet.GetGroupNameByLogin(returnIssue.IssueDetail?.assignee);
            return returnIssue;
        }

        /// <summary>
        /// Enrich the issue with it's corresponding line count (calculated from the ChangeSet).
        /// </summary>
        /// <param name="issue">The current issue.</param>
        /// <param name="tfsConnector">The access to the TFS Services.</param>
        /// <returns>The enriched issue.</returns>
        private static Issue AddCodeLineCount(this Issue issue, ITfsConnector tfsConnector)
        {
            Issue returnIssue = issue;

            if (issue.ChangetSet == default(int))
            {
                returnIssue.CodeLineCountForChangeSet = default(int);
            }
            else
            {
                returnIssue.CodeLineCountForChangeSet = tfsConnector.GetNumberOfImpactedLines(returnIssue.ChangetSet).GetAwaiter().GetResult();
            }

            return returnIssue;
        }

        /// <summary>
        /// Add the CodeProject informations to existing Sonar Issue.
        /// </summary>
        /// <param name="issue">The current Issue.</param>
        /// <param name="tfsConnector">The access to the Tfs Services.</param>
        /// <returns>The enriched Sonar Issue.</returns>
        private static Issue AddCodeProject(this Issue issue, ITfsConnector tfsConnector)
        {
            Issue returnIssue = issue;

            if (issue.ChangetSet == default(int))
            {
                returnIssue.CodeProject = "no changeset detected";
            }
            else
            {
                returnIssue.CodeProject = tfsConnector.GetAgressoCode(issue.ChangetSet).GetAwaiter().GetResult();
            }

            return returnIssue;
        }

        /// <summary>
        /// Add the ChangeSet informations to an existing Sonar Issue.
        /// </summary>
        /// <param name="issue">The current Issue.</param>
        /// <param name="sonarConnector">The access to the Sonar Services.</param>
        /// <returns>The enriched Sonar Issue.</returns>
        private static Issue AddChangeSet(this Issue issue, ISonarConnector sonarConnector)
        {
            Issue returnIssue = issue;

            Source ChangeSetSource = issue.GetChangeSetAsync(sonarConnector);
            returnIssue.ChangetSet = ChangeSetSource?.ChangeSet ?? default(int);
            returnIssue.ChangeSetDate = ChangeSetSource?.CreationDate ?? null;

            return returnIssue;
        }

        /// <summary>
        /// Call the SonarConnector and get the <see cref="Source"/> corresponding to the actual issue informations.
        /// </summary>
        /// <param name="sonarRequestGetChangeSet">The Issue informations used to detect the ChangeSet.</param>
        /// <param name="sonarConnector"></param>
        /// <returns>The <see cref="Source"/> containing the ChangeSet id, it's Date, ...</returns>
        private static Source GetChangeSetAsync(this Issue issue, ISonarConnector sonarConnector)
        {
            if (issue == null)
            {
                return null;
            }

            SonarRequestGetChangeSet sonarRequestGetChangeSet = new SonarRequestGetChangeSet()
            {
                AuthorEmail = issue.IssueDetail.author,
                IssueDate = issue.IssueDetail.creationDate,
                Component = issue.IssueDetail.component,
                Line = issue.IssueDetail.line
            };

            return sonarConnector.GetChangeSet(sonarRequestGetChangeSet);
        }
    }
}
