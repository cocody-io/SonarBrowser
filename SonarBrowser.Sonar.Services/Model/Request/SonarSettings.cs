using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Services.Model.Request
{
    public class SonarSettings
    {
        public string Token { get; set; }
        public string Uri_GetIssues { get; set; }
        public string Uri_GetChangeSet { get; set; }
        public int NbIssuesPerRequestDefault { get; set; }
    }
}
