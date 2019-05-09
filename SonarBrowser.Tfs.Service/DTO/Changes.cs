using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Changes
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("value")]
        public List<ValueItem> Value { get; set; }
    }
}
