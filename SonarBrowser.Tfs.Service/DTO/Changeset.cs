using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class Changeset
    {
        [JsonProperty("changesetId")]
        public int ChangesetId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("author")]
        public Author Author { get; set; }
        [JsonProperty("checkedInBy")]
        public Author CheckinedBy { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("_links")]
        public Links Links { get; set; }
        [JsonProperty("workItems")]
        public List<WorkItem> WitList { get; set; }
    }
}
