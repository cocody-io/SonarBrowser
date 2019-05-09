using System;

namespace SonarBrowser.SonarBrowserOrchestrator.Services.DTO
{
    public class Issue
    {
        public string ADGroup { get; set; }
        public IssueDetail IssueDetail { get; set; }
        public int ChangetSet { get; set; }
        public DateTime? ChangeSetDate { get; set; }
        public string CodeProject { get; set; }
        public int CodeLineCountForChangeSet { get; set; }
    }
}
