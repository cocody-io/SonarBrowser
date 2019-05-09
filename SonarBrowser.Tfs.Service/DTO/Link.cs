using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}