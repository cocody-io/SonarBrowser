using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Item
    {
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("hashValue")]
        public string HashValue { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("isFolder")]
        public bool IsFolder { get; set; }
    }
}