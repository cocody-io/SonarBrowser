using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class ValueItem
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }
    }
}