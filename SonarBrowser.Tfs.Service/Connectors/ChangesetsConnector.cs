using Newtonsoft.Json;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Tfs.Service.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Tfs.Service.Connectors
{
    public class ChangesetsConnector : BaseConnector
    {
        private readonly string _apiUrl = "_apis/tfvc/changesets/";

        public ChangesetsConnector(TfsSettings tfsSettings, IHttpApiClient httpApiClient) : base(tfsSettings, httpApiClient) { }

        public async Task<Changeset> GetChangeset(string changesetId)
        {
            string requestedUrl = TfsUrlBuilder(_apiUrl + changesetId + "?includeDetails=true&includeWorkItems=true");
            return await _httpApiClient.GetAsync<Changeset>(requestedUrl, base.tfsSettings.Token);
        }

        public async Task<Changes> GetChangesetChanges(string changesetId)
        {
            string requestedUrl = TfsUrlBuilder(_apiUrl + changesetId + "/changes");
            return await _httpApiClient.GetAsync<Changes>(requestedUrl, base.tfsSettings.Token);
        }
    }
}
