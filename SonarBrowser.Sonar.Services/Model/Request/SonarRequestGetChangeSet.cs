using System;

namespace SonarBrowser.Services.DTO
{
    public class SonarRequestGetChangeSet
    {
        public int Line { get; set; }

        public string Component { get; set; }

        public string AuthorEmail { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
