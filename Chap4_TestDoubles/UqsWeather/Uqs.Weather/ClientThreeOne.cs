using AdamTibi.OpenWeather;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Uqs.Weather
{
    public class ClientThreeOne : IClient
    {
        private readonly string _apiKey;

        private readonly HttpClient _httpClient;

        private const string BASE_URL = "https://api.openweathermap.org/data/3.0";

        public ClientThreeOne(string apiKey, HttpClient httpClient)
        {
            _apiKey = apiKey;
            _httpClient = httpClient;
        }

        public async Task<OneCallResponse> OneCallAsync(decimal latitude, decimal longitude, IEnumerable<Excludes> excludes, Units unit)
        {
            UriBuilder uriBuilder = new UriBuilder("https://api.openweathermap.org/data/3.0/onecall");
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString("");
            nameValueCollection["lat"] = latitude.ToString();
            nameValueCollection["lon"] = longitude.ToString();
            nameValueCollection["appid"] = _apiKey;
            if (excludes != null && excludes.Any())
            {
                nameValueCollection["exclude"] = string.Join(',', excludes).ToLower();
            }

            nameValueCollection["units"] = unit.ToString().ToLower();
            //Console.Out.WriteLine($"\nQuery: {nameValueCollection.ToString()}\n");
            uriBuilder.Query = nameValueCollection.ToString();
            //Console.Out.WriteLine($"\nUri: {uriBuilder.Uri.AbsoluteUri}\n");
            string value = await _httpClient.GetStringAsync(uriBuilder.Uri.AbsoluteUri);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("No response from the service");
            }

            return JsonConvert.DeserializeObject<OneCallResponse>(value) ?? throw new Exception();
        }
    }
}
