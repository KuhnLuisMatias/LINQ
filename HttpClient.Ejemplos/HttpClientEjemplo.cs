using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientEjemplos
{
    internal class HttpClientEjemplo
    {
        private const string NorthwindUrl ="http://services.data.org/Northwind/Northwind.svc/Regions";
        private const string IncorrectUrl ="http://services.data.org/Northwind1/Northwind.svc/Regions";
        private HttpClient _httpClient;
        public HttpClient HttpClient =>
        _httpClient ?? (_httpClient = new HttpClient());
        private async Task GetDataSimpleAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(NorthwindUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} " +
                $"{response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }
    }
}
}
