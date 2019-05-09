using MoreLinq;
using SonarBrowser.Infrastructure.Cache;
using SonarBrowser.Infrastructure.Logging;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Services.DTO;
using SonarBrowser.Sonar.Service.Model;
using SonarBrowser.Sonar.Services.Model.DTO;
using SonarBrowser.Sonar.Services.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Service
{
    public class SonarConnector : ISonarConnector
    {
        private readonly IHttpApiClient _httpApiClient;
        private readonly ICacheManager _cacheManager;
        private readonly ILoggingService _loggingService;
        private SonarSettings _sonarSettings { get; set; }

        public SonarConnector(SonarSettings sonarSettings, ICacheManager cacheManager, IHttpApiClient httpApiClient, ILoggingService loggingService)
        {
            _cacheManager = cacheManager;
            _httpApiClient = httpApiClient;
            _sonarSettings = sonarSettings;
            _loggingService = loggingService;
        }

        public async Task<SearchIssuesResponse> GetIssuesAsync(SonarRequestGetIssues sonarSettingRequest)
        {
            if (sonarSettingRequest == null || _sonarSettings == null) return null;
            if (sonarSettingRequest.NbIssuesPerRequest == default(int)) sonarSettingRequest.NbIssuesPerRequest = _sonarSettings.NbIssuesPerRequestDefault;

            SearchIssuesResponse searchIssuesResponse = new SearchIssuesResponse();
            searchIssuesResponse.issues = new List<Issues>();
            bool needToRetrieveNextIssues = true;
            int pageNumber = 1;
            while (needToRetrieveNextIssues)
            {
                List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("ps", sonarSettingRequest.NbIssuesPerRequest.ToString()),
                    new KeyValuePair<string, string>("assigned", "true"),
                    new KeyValuePair<string, string>("p", pageNumber.ToString()),
                    new KeyValuePair<string, string>("createdAfter", sonarSettingRequest.DateFrom.ToString("yyyy-MM-dd")),// "2017-10-01"));
                    new KeyValuePair<string, string>("createdBefore", sonarSettingRequest.DateTo.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>("statuses", "OPEN,REOPENED"),
                    new KeyValuePair<string, string>("severities", "CRITICAL, MAJOR"),
                    new KeyValuePair<string, string>("languages", "cs")
                };
                if (sonarSettingRequest.Users != null)
                {
                    args.Add(new KeyValuePair<string, string>("assignees", string.Join(",", sonarSettingRequest.Users)));
                }
                SearchIssuesResponse result = await _httpApiClient.PostAsync<SearchIssuesResponse>(args, _sonarSettings.Uri_GetIssues, _sonarSettings.Token);
                if (result?.issues == null) break;
                searchIssuesResponse.issues.AddRange(result.issues);
                needToRetrieveNextIssues = sonarSettingRequest.NbIssuesPerRequest == result.issues.Count;
                pageNumber++;
            }

            return searchIssuesResponse;
        }

        public Source GetChangeSet(SonarRequestGetChangeSet sonarRequestGetChangeSet)
        {
            if (sonarRequestGetChangeSet == null || _sonarSettings == null) return null;

            try
            {
                string cacheKey = string.Format("GetChangesetRespByComponent_{0}", sonarRequestGetChangeSet.Component);
                var listIssues = _cacheManager.Resolve(cacheKey, async () =>
                {
                    return await GetSourceFromSonar(sonarRequestGetChangeSet);
                });

                if (sonarRequestGetChangeSet.Line == default(int))
                {
                    return listIssues.GetAwaiter().GetResult()?.Where(_ => _.AuthorEmail == sonarRequestGetChangeSet.AuthorEmail)
                                                    ?.MinBy(_ => Math.Abs((_.CreationDate - sonarRequestGetChangeSet.IssueDate).Ticks));
                }
                else
                {
                    return listIssues.GetAwaiter().GetResult()?.FirstOrDefault(_ => _.Line == sonarRequestGetChangeSet.Line);
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError(sonarRequestGetChangeSet, "", ex);
                return null;
            }

        }


        private SearchIssuesResponse SetDefaultLineValueIfNoLineNumber(SearchIssuesResponse searchIssuesResponse)
        {
            if (searchIssuesResponse?.issues == null) return searchIssuesResponse;
            var issuesWithNoLineNumber = searchIssuesResponse.issues.Where(_ => _.line == 0);
            foreach (var currentIssue in issuesWithNoLineNumber)
            {
                currentIssue.line = default(int);
            }
            return searchIssuesResponse;
        }

        private async Task<List<Source>> GetSourceFromSonar(SonarRequestGetChangeSet sonarRequestGetChangeSet)
        {
            try
            {
                if (sonarRequestGetChangeSet == null || _sonarSettings == null) return new List<Source>();

                List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", sonarRequestGetChangeSet.Component)
            };

                LinesResponse responseLines = await _httpApiClient.PostAsync<LinesResponse>(args, _sonarSettings.Uri_GetChangeSet, _sonarSettings.Token);

                List<Source> res = new List<Source>();
                if (responseLines?.sources != null)
                {
                    foreach (var item in responseLines.sources)
                    {
                        res.Add(new Source() { ChangeSet = item.ChangeSet, Line = item.Line, AuthorEmail = item.Author, CreationDate = item.CreationDate });
                    }
                }
                return res;
            }
            catch
            {
                return null;
            }

        }
    }
}
