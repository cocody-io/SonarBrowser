using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Services.Model.DTO
{
    public class LinesResponse
    {
        [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
        public List<LinesDetails> sources { get; set; }
    }
}
