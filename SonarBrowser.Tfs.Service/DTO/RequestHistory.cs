using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class RequestHistory
    {
        [JsonProperty("itemPath")]
        public string ItemPath { get; set; }
        [JsonProperty("itemVersion")]
        public string ItemVersion { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
    }
}
