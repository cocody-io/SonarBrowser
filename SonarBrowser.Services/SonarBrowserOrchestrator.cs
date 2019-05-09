using AutoMapper;
using SonarBrowser.ActiveDirectory.Service;
using SonarBrowser.ActiveDirectory.Service.Interface;
using SonarBrowser.Infrastructure.Logging;
using SonarBrowser.Services.DTO;
using SonarBrowser.Sonar.Service;
using SonarBrowser.Sonar.Service.Model;
using SonarBrowser.SonarBrowserOrchestrator.Services.Assembler;
using SonarBrowser.SonarBrowserOrchestrator.Services.DTO;
using SonarBrowser.Tfs.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SonarBrowser.SonarBrowserOrchestrator.Services
{
    /// <summary>
    /// Main Service for SonarBrowser related searches.
    /// </summary>
    public class SonarBrowserOrchestrator : ISonarBrowserOrchestrator
    {
        private readonly ISonarConnector _sonarConnector;
        private readonly IActiveDirectoryService _activeDirectoryService;
        private readonly ITfsConnector _tfsConnector;
        private readonly IMapper _mapper;
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// Constructor for the SonarBrowser Orchestrator.
        /// </summary>
        /// <param name="sonarConnector">Connector for SonarQube related searches.</param>
        /// <param name="mapper">AutoMapper interface.</param>
        /// <param name="activeDirectoryService">Service for ActiveDirectory related searches.</param>
        /// <param name="tfsConnector">Service for Tfs related searches.</param>
        /// <param name="loggingService">The error logging services.</param>
        public SonarBrowserOrchestrator(ISonarConnector sonarConnector, IMapper mapper, IActiveDirectoryService activeDirectoryService, ITfsConnector tfsConnector, ILoggingService loggingService)
        {
            _sonarConnector = sonarConnector;
            _mapper = mapper;
            _activeDirectoryService = activeDirectoryService;
            _tfsConnector = tfsConnector;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Get Sonar issues corresponding to the <see cref="SonarRequestGetIssues"/> parameters.
        /// </summary>
        /// <param name="sonarSettingRequest">The parameters used to get the issues.</param>
        /// <returns>A list of Sonar issues with ActiveDirectory, ChangeSet and CodeProject informations.</returns>
        public IssuesSonarSet GetIssuesSonar(SonarRequestGetIssues sonarSettingRequest)
        {
            if (sonarSettingRequest == null)
            {
                return null;
            }

            _loggingService.LogInfo(this, "Parameters : DateFrom " + sonarSettingRequest.DateFrom.ToString() + ", DateTo " + sonarSettingRequest.DateFrom.ToString());

            GroupADSet groupADSet = _activeDirectoryService.GetUsersByGroupAd(sonarSettingRequest.GroupAdSet);
            sonarSettingRequest.Users = groupADSet?.GetListOfUser();

            SearchIssuesResponse searchIssuesResponse = _sonarConnector.GetIssuesAsync(sonarSettingRequest).Result;
            List<Issue> issueSet = searchIssuesResponse.CreateIssueSet(groupADSet, _mapper, _sonarConnector, _tfsConnector);

            _loggingService.LogInfo(this, issueSet.Count().ToString() + " resultats. Parameters : DateFrom " + sonarSettingRequest.DateFrom.ToString() + ", DateTo " + sonarSettingRequest.DateTo.ToString());

            return new IssuesSonarSet()
            {
                IssueSet = issueSet
            };
        }
    }
}
