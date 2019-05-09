using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Links
    {
        [JsonProperty("self")]
        public Link Self { get; set; }
        [JsonProperty("changes")]
        public Link Changes { get; set; }
        [JsonProperty("workItems")]
        public Link WorkItems { get; set; }
        [JsonProperty("web")]
        public Link Web { get; set; }
        [JsonProperty("author")]
        public Link Author { get; set; }
        [JsonProperty("checkedInBy")]
        public Link CheckinedBy { get; set; }
    }
}