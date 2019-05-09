using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class SearchHistory
    {
        [JsonProperty("results")]
        public List<ResultHistory> Results { get; set; }
        [JsonProperty("moreResultsAvailable")]
        public bool MoreResultsAvailable { get; set; }
    }
}
