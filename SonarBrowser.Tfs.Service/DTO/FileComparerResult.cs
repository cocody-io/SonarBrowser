using Newtonsoft.Json;
using System.Collections.Generic;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class FileComparerResult
    {
        [JsonProperty("blocks")]
        public List<Block> Blocks { get; set; }
        [JsonProperty("lindeCharBlocks")]
        public List<object> LineCharBlocks { get; set; }
        [JsonProperty("modifiedFile")]
        public TfsFile ModifiedFile { get; set; }
        [JsonProperty("originalFile")]
        public TfsFile OriginalFile { get; set; }
    }
}
