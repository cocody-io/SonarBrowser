using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Services.Model.DTO
{
    public class LinesDetails
    {
        [JsonProperty("line", NullValueHandling = NullValueHandling.Ignore)]
        public int Line { get; set; }

        [JsonProperty("scmRevision", NullValueHandling = NullValueHandling.Ignore)]
        public int ChangeSet { get; set; }

        [JsonProperty("scmAuthor", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty("scmDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreationDate { get; set; }
    }
}
