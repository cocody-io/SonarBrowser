using SonarBrowser.Services.DTO;
using SonarBrowser.SonarBrowserOrchestrator.Services;
using SonarBrowser.Tfs.Service;
using SonarBrowser.WebSite.Factories.Interfaces;
using SonarBrowser.WebSite.ViewModel;
using System;
using System.Configuration;
using System.Linq;

namespace SonarBrowser.WebSite.Factories
{
    public class IssuesSonarViewModelFactory : IIssuesSonarViewModelFactory
    {
        private readonly ISonarBrowserOrchestrator _sonarService;
        private SonarRequestGetIssues _sonarRequestGetIssues;

        private TfsSettings _tfsSettings;

        public IssuesSonarViewModelFactory(ISonarBrowserOrchestrator sonarService)
        {
            _sonarService = sonarService;
            InitSettings();
        }

        public IssuesSonarViewModel BuildIssuesSonarViewModel(DateTime DateFrom, DateTime DateTo)
        {
            _sonarRequestGetIssues.DateFrom = DateFrom;
            _sonarRequestGetIssues.DateTo = DateTo;
            var result = _sonarService.GetIssuesSonar(_sonarRequestGetIssues);
            IssuesSonarViewModel issuesSonarViewModel = new IssuesSonarViewModel() { Issues = result, DateFrom = DateFrom, DateTo = DateTo };
            issuesSonarViewModel.Issues = result;
            return issuesSonarViewModel;
        }

        private void InitSettings()
        {
            InitSonarRequest();
            InitTfsSettings();
        }

        private void InitSonarRequest()
        {
            _sonarRequestGetIssues = new SonarRequestGetIssues();
            _sonarRequestGetIssues.GroupAdSet = ConfigurationManager.AppSettings["GroupAdSet"].Split(',').ToList();
        }

        private void InitTfsSettings()
        {
            _tfsSettings = new TfsSettings();
            _tfsSettings.Token = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", ConfigurationManager.AppSettings["personalAccessTokenTfs"])));
            _tfsSettings.Url = ConfigurationManager.AppSettings["tfsUrl"];
        }
    }
}
