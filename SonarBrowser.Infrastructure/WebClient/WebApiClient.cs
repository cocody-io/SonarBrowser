using Newtonsoft.Json;
using SonarBrowser.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Infrastructure.WebClient
{
    public class WebApiClient : IHttpApiClient
    {
        private static HttpClient _httpClient;
        private readonly ILoggingService _loggingService;

        public WebApiClient(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }
       

        public async Task<TResult> PostAsync<TResult>(List<KeyValuePair<string, string>> ArgsBodyRequest, string Uri, string authorizationToken, int timeOut = 0) where TResult : new()
        {
            InitHttpClient(authorizationToken);
            HttpResponseMessage response = null;
            CallWebApi(
                () =>
                    {
                        try
                        {
                            response = _httpClient.PostAsync(Uri, new FormUrlEncodedContent(ArgsBodyRequest)).Result;
                            if (response.IsSuccessStatusCode == false)
                                _loggingService.LogError(this, "error response API code return : " + response.StatusCode + " . Details response : " + JsonConvert.SerializeObject(response)); 
                        }
                        catch (TaskCanceledException ex)
                        {
                            _loggingService.LogError(this, "", ex);
                        }
                    }
                );
            return await response.Content.ReadAsAsync<TResult>();
        }


        public async Task<TResult> GetAsync<TResult>(string Uri, string AuthorizationToken, int timeOut = 0) where TResult : new()
        {
            InitHttpClient(AuthorizationToken);
            HttpResponseMessage response = null;
            CallWebApi(
                () =>
                {
                    try
                    {
                        response = _httpClient.GetAsync(Uri).Result;
                        if (response.IsSuccessStatusCode == false)
                            _loggingService.LogError(this, "error response API code return : " + response.StatusCode + " . Details response : " + JsonConvert.SerializeObject(response));
                    }
                    catch (TaskCanceledException ex)
                    {
                        _loggingService.LogError(this, "", ex);
                    }
                }
                );
            return await response.Content.ReadAsAsync<TResult>();
        }



        private string CallWebApi(Action a, bool rethrow = true)
        {
            string result = null;
            try
            {
                a();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                    result = reader.ReadToEnd();
                    if (rethrow)
                    {
                        throw new WebException(string.Format("Call API error: {0}", result), ex);
                    }
                }
                else
                {
                    throw;
                }
            }
            return result;
        }

        private void InitHttpClient(string authorizationToken)
        {
            if(_httpClient==null) _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + authorizationToken);
        }
    }
}
