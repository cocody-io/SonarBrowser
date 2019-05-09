using Newtonsoft.Json;
using System;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class TfsFile
    {
        [JsonProperty("changeDate")]
        public DateTime ChangeDate { get; set; }
        [JsonProperty("contentMetadata")]
        public TfsFileMetadata ContentMetadata { get; set; }
        [JsonProperty("serverItem")]
        public string ServerItem { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("versionDescription")]
        public string VersionDescription { get; set; }
        [JsonProperty("changeset")]
        public int Changeset { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}