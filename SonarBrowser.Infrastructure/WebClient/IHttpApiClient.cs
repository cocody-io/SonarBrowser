using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Infrastructure.WebClient
{
    public interface IHttpApiClient
    {
        Task<TResult> PostAsync<TResult>(List<KeyValuePair<string, string>> ArgsBodyRequest, string Uri_GetChangeSet, string authorizationToken, int timeOut = 0) where TResult : new();

        Task<TResult> GetAsync<TResult>(string Uri, string AuthorizationToken, int timeOut = 0) where TResult : new();
    }
}
