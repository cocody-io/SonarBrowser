using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class RequestFileCompare
    {
        [JsonProperty("originalPath")]
        public string OriginalPath { get; set; }
        [JsonProperty("originalVersion")]
        public string OriginalVersion { get; set; }
        [JsonProperty("modifiedPath")]
        public string ModifiedPath { get; set; }
        [JsonProperty("modifiedVersion")]
        public string ModifiedVersion { get; set; }
        [JsonProperty("partialDif")]
        public bool PartialDiff { get; set; }
        [JsonProperty("includeCharDiffs")]
        public bool IncludeCharDiffs { get; set; }
    }
}
