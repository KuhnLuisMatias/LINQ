using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientEjemplos
{
    public class HttpClientEjemplo : IDisposable
    {
        private const string URL_VALIDA = "https://jsonplaceholder.typicode.com/posts";
        private const string URL_INVALIDA = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

        private HttpClient _httpClient;
        private HttpClient _httpClientWithMessageHandler;
        public HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());
        public HttpClient HttpClientWithMessageHandler => _httpClientWithMessageHandler ?? (_httpClientWithMessageHandler = new HttpClient(new SampleMessageHandler("error")));

        public async Task GetDataSimpleAsync()
        {
            /*

            La función GetDataSimpleAsync() es un ejemplo de cómo se puede utilizar 
            la clase HttpResponseMessage para realizar una solicitud HTTP GET a una URL específica (especificada por la constante URL_VALIDA) 
            utilizando el objeto HttpClient.
            La primera línea de código HttpResponseMessage response = await HttpClient.GetAsync(URL_VALIDA); 
            hace una llamada asíncrona al método GetAsync del objeto HttpClient, 
            pasando la URL a la que se desea hacer la solicitud. 
            El resultado de esta llamada es asignado a la variable "response" de tipo HttpResponseMessage.
            La siguiente línea de código if (response.IsSuccessStatusCode) 
            comprueba si la respuesta obtenida tiene un código de estado HTTP 2xx o 3xx, 
            lo que indica que la solicitud fue exitosa. Si se cumple esta condición se entra al bloque de código dentro del "if".

            Dentro del bloque "if" tenemos varias líneas de código que muestran información sobre la respuesta obtenida:

            a. Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}"); 
            imprime el código de estado HTTP y la razón (frase) correspondiente a ese código.
            
            b. string responseBodyAsText = await response.Content.ReadAsStringAsync(); 
            lee el contenido de la respuesta en formato de texto y lo asigna a una variable "responseBodyAsText".
            
            c. Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters"); 
            imprime el tamaño del contenido de la respuesta en caracteres.
            
            d. Console.WriteLine(); imprime una línea en blanco.
            e. Console.WriteLine(responseBodyAsText); imprime el contenido de la respuesta.

            En resumen este código realiza una solicitud GET a una URL especifica, verifica si la solicitud fue exitosa y en caso afirmativo ,imprime información sobre la respuesta obtenida, como el código de estado, el contenido y su longitud.
            */

            HttpResponseMessage response = await HttpClient.GetAsync(URL_VALIDA);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }
        public async Task GetDataAdvancedAsync()
        {
            /*
            La primera línea de código var request = new HttpRequestMessage(HttpMethod.Get, URL_VALIDA); crea una nueva instancia de la clase HttpRequestMessage, utilizando el método HttpMethod.Get y la URL especificada por la constante URL_VALIDA. El objeto HttpRequestMessage contiene información sobre la solicitud HTTP que se va a enviar, como el método HTTP, la URL, los encabezados y el cuerpo de la solicitud.
            La segunda línea HttpResponseMessage response = await HttpClient.SendAsync(request); hace una llamada asíncrona al método SendAsync del objeto HttpClient, pasando el objeto HttpRequestMessage creado anteriormente como parámetro. El resultado de esta llamada es asignado a la variable "response" de tipo HttpResponseMessage. El objeto HttpResponseMessage contiene información sobre la respuesta HTTP recibida, como el código de estado, los encabezados y el cuerpo de la respuesta.
            En resumen, este código crea un objeto HttpRequestMessage con un método GET y una URL específica, y luego utiliza el objeto HttpClient para enviar la solicitud y obtener una respuesta. La respuesta se almacena en una variable de tipo HttpResponseMessage, que contiene información sobre la respuesta recibida.
            */
            var request = new HttpRequestMessage(HttpMethod.Get, URL_VALIDA);
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }
        public async Task GetDataWithExceptionsAsync()
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);
                HttpResponseMessage response = await HttpClient.GetAsync(URL_INVALIDA);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        public async Task GetDataWithHeadersAsync()
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);

                HttpResponseMessage response = await HttpClient.GetAsync(URL_VALIDA);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        public async Task GetDataWithMessageHandlerAsync()
        {
            try
            {
                HttpClientWithMessageHandler.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClientWithMessageHandler.DefaultRequestHeaders);

                HttpResponseMessage response = await HttpClientWithMessageHandler.GetAsync(URL_VALIDA);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        public void ShowHeaders(string title, HttpHeaders headers)
        {
            Console.WriteLine(title);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                Console.WriteLine($"Header: {header.Key} Value: {value}");
            }
            Console.WriteLine();
        }
        public void Dispose()
        {
            HttpClient?.Dispose();
            HttpClientWithMessageHandler?.Dispose();
        }
    }
}
