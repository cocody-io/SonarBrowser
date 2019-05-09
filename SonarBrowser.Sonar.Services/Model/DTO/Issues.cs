using Newtonsoft.Json;
using System;

namespace SonarBrowser.Sonar.Service.Model
{
    public class Issues
    {
   

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        [JsonProperty("rule", NullValueHandling = NullValueHandling.Ignore)]
        public string rule { get; set; }

        [JsonProperty("severity", NullValueHandling = NullValueHandling.Ignore)]
        public string severity { get; set; }

        [JsonProperty("component", NullValueHandling = NullValueHandling.Ignore)]
        public string component { get; set; }

        [JsonProperty("componentId", NullValueHandling = NullValueHandling.Ignore)]
        public int componentId { get; set; }

        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public string project { get; set; }

        [JsonProperty("subProject", NullValueHandling = NullValueHandling.Ignore)]
        public string subProject { get; set; }

        [JsonProperty("resolution", NullValueHandling = NullValueHandling.Ignore)]
        public string resolution { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        [JsonProperty("effort", NullValueHandling = NullValueHandling.Ignore)]
        public string effort { get; set; }

        [JsonProperty("debt", NullValueHandling = NullValueHandling.Ignore)]
        public string debt { get; set; }

        [JsonProperty("assignee", NullValueHandling = NullValueHandling.Ignore)]
        public string assignee { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string author { get; set; }

        [JsonProperty("creationDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime creationDate { get; set; }

        [JsonProperty("updateDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime updateDate { get; set; }

        [JsonProperty("closeDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime closeDate { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        [JsonProperty("line", NullValueHandling = NullValueHandling.Ignore)]
        public int line { get; set; }
    }
}
