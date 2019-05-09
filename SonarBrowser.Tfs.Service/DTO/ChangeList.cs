using Newtonsoft.Json;
using System;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class ChangeList
    {
        [JsonProperty("changesetId")]
        public int ChangesetId { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("ownerDisplayName")]
        public string OwnerDisplayName { get; set; }
        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }
        [JsonProperty("sortDate")]
        public DateTime SortDate { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}