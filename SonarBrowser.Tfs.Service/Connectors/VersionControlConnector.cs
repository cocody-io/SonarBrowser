using Newtonsoft.Json;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Tfs.Service.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SonarBrowser.Tfs.Service.Connectors
{
    public class VersionControlConnector : BaseConnector
    {
        private readonly string path = "_api/_versioncontrol/";

        public VersionControlConnector(TfsSettings tfsSettings, IHttpApiClient httpApiClient) : base(tfsSettings, httpApiClient) { }

        public async Task<SearchHistory> FileHistory(string filePath, string itemVersion)
        {
            RequestHistory req = new RequestHistory
            {
                ItemPath = filePath,
                ItemVersion = "T",
                Top = 200
            };
            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>();
            args.Add(new KeyValuePair<string, string>("searchCriteria", JsonConvert.SerializeObject(req)));
            return await base._httpApiClient.PostAsync<SearchHistory>(args, TfsUrlBuilder(path + "history"),tfsSettings.Token);
        }

        public async Task<FileComparerResult> CompareChangesetFile(string filePath, string firstChangesetId, string secondChangesetId)
        {
            FileComparerResult result = new FileComparerResult();
            RequestFileCompare req = new RequestFileCompare
            {
                OriginalPath = filePath,
                OriginalVersion = firstChangesetId,
                ModifiedPath = filePath,
                ModifiedVersion = secondChangesetId,
                IncludeCharDiffs = true,
                PartialDiff = true
            };
            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>();
            args.Add(new KeyValuePair<string, string>("diffParameters", JsonConvert.SerializeObject(req)));
            return await base._httpApiClient.PostAsync<FileComparerResult>(args, TfsUrlBuilder(path + "fileDiff"), tfsSettings.Token);
        }

        public async Task<TfsContentFile> GetItemContentJson(string filePath, int version)
        {
            string customPath = System.Uri.EscapeDataString(filePath);
            string formatUrl = "path={0}&version={1}&includeBinaryContent=true&splitContentIntoLines=true";
            string url = path + "itemContentJson?" + string.Format(formatUrl, customPath, version);
            return await _httpApiClient.GetAsync<TfsContentFile>(url, base.tfsSettings.Token);
        }
    }
}
