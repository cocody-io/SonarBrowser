using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Sonar.Service.Model
{
    public class SearchIssuesResponse
    {
        [JsonProperty("issues", NullValueHandling = NullValueHandling.Ignore)]
        public List<Issues> issues { get; set; }
    }
}
