using System;

namespace SonarBrowser.SonarBrowserOrchestrator.Services.DTO
{
    public class IssueDetail
    {
        public string key { get; set; }

        public string rule { get; set; }

        public string severity { get; set; }

        public string component { get; set; }

        public int componentId { get; set; }

        public string project { get; set; }

        public string subProject { get; set; }

        public string resolution { get; set; }

        public string status { get; set; }

        public string message { get; set; }

        public string effort { get; set; }

        public string debt { get; set; }

        public string assignee { get; set; }

        public string author { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime updateDate { get; set; }

        public DateTime closeDate { get; set; }

        public string type { get; set; }

        public int line { get; set; }
    }
}
