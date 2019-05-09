using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class WorkItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("rev")]
        public int Revision { get; set; }
        [JsonProperty("fields")]
        public WitFields WitFields { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
