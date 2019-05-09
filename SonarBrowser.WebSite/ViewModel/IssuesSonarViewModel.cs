using SonarBrowser.SonarBrowserOrchestrator.Services.DTO;
using System;

namespace SonarBrowser.WebSite.ViewModel
{
    public class IssuesSonarViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public IssuesSonarSet Issues { get; set; }
    }
}
