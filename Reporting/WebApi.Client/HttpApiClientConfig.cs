using System;
using System.Net.Http.Headers;
using System.Text.Json;
using Empiria.Trade.Reporting.WebApi.Client.Adapters;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

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


    internal async Task<T> DeserializeObject<T>(T t, JsonElement data) {

      try {

        return await Task.FromResult(JsonSerializer.Deserialize<T>(
          data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        ));

      } catch (Exception e) {

        throw new Exception($"{e.Message}");
      }

    }


    internal async Task<List<T>> DeserializeObjectList<T>(List<T> t, JsonElement data) {
      try {

        return await Task.FromResult(JsonSerializer.Deserialize<List<T>>(
          data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        ) ?? new List<T>());

      } catch (Exception e) {

        throw new Exception($"{e.Message}");
      }
    }


    internal async Task<JsonElement> GetJsonContent(HttpClient http, string uri) {

      var response = await http.GetAsync(uri).ConfigureAwait(false);
      var content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
      return content.RootElement.GetProperty("data");
    }


  }
}
