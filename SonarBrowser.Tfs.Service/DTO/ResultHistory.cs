using Newtonsoft.Json;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class ResultHistory
    {
        [JsonProperty("changeList")]
        public ChangeList ChangeList { get; set; }
        [JsonProperty("encoding")]
        public int Encoding { get; set; }
        [JsonProperty("fileId")]
        public int FileId { get; set; }
        [JsonProperty("itemChangeType")]
        public int ItemChangeType { get; set; }
        [JsonProperty("serverItem")]
        public string ServerItem { get; set; }
    }
}