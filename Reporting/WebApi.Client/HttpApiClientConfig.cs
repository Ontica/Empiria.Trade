using System.Net.Http.Headers;
using System.Text.Json;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.Extensions.Configuration;

namespace Empiria.Trade.Reporting.WebApi.Client {


    public class HttpApiClientConfig {

        private readonly HttpClient httpClient = new HttpClient();
        private readonly string BaseAddress;
        private readonly TimeSpan TimeOut;

        public HttpApiClientConfig(string baseAddress, TimeSpan timeOut) {

            BaseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";
            TimeOut = timeOut;

        }


        public HttpClient HttpApiClient() {

            try {
                httpClient.BaseAddress = new Uri(BaseAddress);
                httpClient.Timeout = TimeOut;

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;

            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }


        internal async Task<T> DeserializeObject<T>(T singleObject, HttpClient http, string uri) {

            var response = await http.GetAsync(uri).ConfigureAwait(false);

            var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var data = content.RootElement.GetProperty("data");

            singleObject = data.Deserialize<T>();

            return await Task.FromResult(singleObject).ConfigureAwait(false);
        }


        internal async Task<List<T>> DeserializeObjectList<T>(List<T>? objectList, HttpClient http, string uri) {
            
            var response = await http.GetAsync(uri).ConfigureAwait(false);

            var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var data = content.RootElement.GetProperty("data");

            objectList = data.Deserialize<List<T>>() ?? new List<T>();

            return await Task.FromResult(objectList).ConfigureAwait(false) ?? new List<T>();
        }

    }
}
