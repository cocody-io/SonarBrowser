using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class TfsContentFile
    {
        [JsonProperty("contentLines")]
        public List<string> ContentLines { get; set; }
        [JsonProperty("metadata")]
        public TfsFileMetadata Metadata { get; set; }
    }
}
