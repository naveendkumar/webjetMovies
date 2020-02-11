using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebjetMovies.PersistenceLayer.Interfaces;
using WebjetMovies.PersistenceLayer.Model;

namespace WebjetMovies.PersistenceLayer.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpClientService : IHttpClientService
    {

        #region Initialization

        private readonly HttpClient httpClient;
        private AppSettings applicationSettings { get; }

        #endregion

        #region Constructor

        public HttpClientService(IOptions<AppSettings> settings)
        {
            applicationSettings = settings.Value;
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            SetAuthenticationHeader();
        }

        #endregion

        #region Private Methods

        private void SetAuthenticationHeader()
        {
            httpClient.DefaultRequestHeaders.Add("x-access-token", applicationSettings.APIAuthenticationToken);
        }

        #endregion

        #region Interface Implementation methods

        public async Task<T> GetData<T>(string apiUrl)
        {
            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

       

        #endregion
    }
}
