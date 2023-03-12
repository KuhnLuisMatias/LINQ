using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientEjemplos
{
    internal class Program
    {
        //PAG 4.6
        static async Task Main(string[] args)
        {
            HttpClientEjemplo http = new HttpClientEjemplo();

            /*
            await http.GetDataSimpleAsync();
            await http.GetDataAdvancedAsync();
            await http.GetDataWithHeadersAsync();
            await http.GetDataWithExceptionsAsync();
            await http.GetDataWithMessageHandlerAsync();
            */


            Console.ReadLine();
        }
    }
}
