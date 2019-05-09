using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Block
    {
        [JsonProperty("changeType")]
        public int ChangeType { get; set; }
        [JsonProperty("mLine")]
        public int ModifiedLine { get; set; }
        [JsonProperty("mLines")]
        public List<string> ModifiedLines { get; set; }
        [JsonProperty("mLinesCount")]
        public int ModifiedLinesCount { get; set; }
        [JsonProperty("oLine")]
        public int OriginalLine { get; set; }
        [JsonProperty("oLines")]
        public List<string> OriginalLines { get; set; }
        [JsonProperty("oLinesCount")]
        public int OriginalLinesCount { get; set; }
        [JsonProperty("truncatedBefore", NullValueHandling = NullValueHandling.Ignore)]
        public bool TruncatedBefore { get; set; }
    }
}