using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Tfs.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Tfs.Service.Connectors
{
    public class WorkItemConnector : BaseConnector
    {
        private readonly string path = "_apis/wit/workitems/";

        public WorkItemConnector(TfsSettings tfsSettings, IHttpApiClient HttpApiClient) : base(tfsSettings, HttpApiClient) { }

        public async Task<WorkItem> GetWorkItem(string witId)
        {
            WorkItem result = new WorkItem();
            string url = TfsUrlBuilder(path + witId);
            return await _httpApiClient.GetAsync<WorkItem>(url, base.tfsSettings.Token);
        }
    }
}
