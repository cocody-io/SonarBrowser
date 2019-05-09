using System;
using System.Collections.Generic;

namespace SonarBrowser.Services.DTO
{
    public class SonarRequestGetIssues
    {
        public int NbIssuesPerRequest { get; set; }
        public List<string> Users { get; set; }
        public List<string> GroupAdSet { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
