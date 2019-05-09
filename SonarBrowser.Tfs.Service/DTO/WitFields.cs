using Newtonsoft.Json;
using System;

namespace SonarBrowser.Tfs.Service.DTO
{
    public class WitFields
    {
        [JsonProperty("System.AreaPath")]
        public string AreaPath { get; set; }
        [JsonProperty("System.TeamProject")]
        public string TeamProject { get; set; }
        [JsonProperty("System.IterationPath")]
        public string IterationPath { get; set; }
        [JsonProperty("System.WorkItemType")]
        public string WorkItemType { get; set; }
        [JsonProperty("System.State")]
        public string WitState { get; set; }
        [JsonProperty("System.Reason")]
        public string Reason { get; set; }
        [JsonProperty("System.AssignedTo")]
        public string AssignedTo { get; set; }
        [JsonProperty("System.CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("System.CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("System.ChangedDate")]
        public DateTime ChangedDate { get; set; }
        [JsonProperty("System.ChangedBy")]
        public string ChangedBy { get; set; }
        [JsonProperty("System.Title")]
        public string Title { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTime StateChangeDate { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.ActivatedDate")]
        public DateTime ActivatedDate { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.ActivatedBy")]
        public string ActivatedBy { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.ClosedDate")]
        public DateTime ClosedDate { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.ClosedBy")]
        public string ClosedBy { get; set; }
        [JsonProperty("Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }
        [JsonProperty("Cdiscount.IdeoProject.ProjectCodeAgresso")]
        public string ProjectCodeAgresso { get; set; }
        [JsonProperty("CDiscount.Common.MantisProject")]
        public string MantisProject { get; set; }
        [JsonProperty("Cdiscount.PCI.Decision")]
        public string Decision { get; set; }
        [JsonProperty("Cdiscount.PCI.CodeValidationUser")]
        public string CodeValidationUser { get; set; }
        [JsonProperty("Cdiscount.Integration.IterationValidated")]
        public string IterationValidated { get; set; }
    }
}