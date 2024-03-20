using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Empiria.Trade.Reporting.WebApi.Client {


    public class HttpApiClientConfig {

        
        private readonly HttpClient httpClient = new HttpClient();

        public HttpApiClientConfig() {
        }

        public HttpApiClientConfig(string baseAddress, TimeSpan timeout) {

            try {
                baseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";

                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.Timeout = timeout;

                this.LoadDefaultHeaders();

            } catch (Exception e) {
                throw new Exception(e.Message);
            }


        }


        public HttpClient HttpApiClient(string baseAddress, TimeSpan timeout) {


            try {
                baseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";

                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.Timeout = timeout;

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;

            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }


        private void LoadDefaultHeaders() {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



    }
}
