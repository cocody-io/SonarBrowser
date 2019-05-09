using Newtonsoft.Json;
using SonarBrowser.Infrastructure.WebClient;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Tfs.Service.Connectors
{
    public abstract class BaseConnector
    {
        protected TfsSettings tfsSettings;
        protected IHttpApiClient _httpApiClient;

        public BaseConnector(TfsSettings TfsSettings, IHttpApiClient httpApiClient)
        {
            tfsSettings = TfsSettings;
            _httpApiClient = httpApiClient;
        }

        public string TfsUrlBuilder(string path)
        {
            return tfsSettings.Url + path;
        }
    }
}
