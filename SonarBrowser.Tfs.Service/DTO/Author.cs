using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Author
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}