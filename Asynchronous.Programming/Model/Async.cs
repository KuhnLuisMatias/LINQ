using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Asynchronous.Programming.Model
{
    public static class Async
    {
        private const string url = "http://www.cninnovation.com";
        private readonly static Dictionary<string, string> names = new Dictionary<string, string>();

        #region History
        public static void SynchronizedAPI()
        {
            Console.WriteLine(nameof(SynchronizedAPI));
            using (var client = new WebClient())
            {
                string content = client.DownloadString(url);
                Console.WriteLine(content.Substring(0, 1000));
            }
            Console.WriteLine();
        }
        public static void AsynchronousPattern()
        {
            Console.WriteLine(nameof(AsynchronousPattern));

            WebRequest request = WebRequest.Create(url);
            IAsyncResult result = request.BeginGetResponse(ReadResponse, null);

            void ReadResponse(IAsyncResult ar)
            {
                using (WebResponse response = request.EndGetResponse(ar))
                {
                    Stream stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                    Console.WriteLine();
                }
            }
        }
        public static void EventBasedAsyncPattern()
        {
            /* 
                La diferencia entre este patrón asíncrono basado en eventos y la programación síncrona 
                es el orden de las llamadas a los métodos; en el patrón asíncrono se invierten. 
                Antes de invocar el método asíncrono, es necesario definir qué ocurre cuando se completa la llamada al método. 
            */

            Console.WriteLine(nameof(EventBasedAsyncPattern));
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    Console.WriteLine(e.Result.Substring(0, 100));
                };

                client.DownloadStringAsync(new Uri(url));
                Console.WriteLine("Async History");
            }
        }
        public static async Task TaskBasedAsyncPatternAsync()
        {
            Console.WriteLine(nameof(TaskBasedAsyncPatternAsync));

            using (var client = new WebClient())
            {
                string content = await client.DownloadStringTaskAsync(url);
                Console.WriteLine(content.Substring(0, 100));
                Console.WriteLine();
            }
        }
        #endregion

        #region AsyncAwait
        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? " no task" : " task " + Task.CurrentId;
            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId}" + $" and {taskInfo}");
        }
        public static string Greeting(string name)
        {
            TraceThreadAndTask($"running {nameof(Greeting)}");
            Task.Delay(3000).Wait();
            return $"Hello, {name}";
        }
        public static Task<string> GreetingAsync(string name) =>
        Task.Run<string>(() =>
        {
            TraceThreadAndTask($"running {nameof(GreetingAsync)}");
            return Greeting(name);
        });
        public async static void CallerWithAsync()
        {
            TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
            string result = await GreetingAsync("Stephanie");
            Console.WriteLine(result);
            TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
        }
        public async static void CallerWithAsync2()
        {
            TraceThreadAndTask($"started {nameof(CallerWithAsync2)}");
            Console.WriteLine(await GreetingAsync("Stephanie"));
            TraceThreadAndTask($"ended {nameof(CallerWithAsync2)}");
        }
        public static void CallerWithAwaiter()
        {
            TraceThreadAndTask($"starting {nameof(CallerWithAwaiter)}");
            TaskAwaiter<string> awaiter = GreetingAsync("Matthias").GetAwaiter();
            awaiter.OnCompleted(OnCompleteAwaiter);

            void OnCompleteAwaiter()
            {
                Console.WriteLine(awaiter.GetResult());
                TraceThreadAndTask($"ended {nameof(CallerWithAwaiter)}");
            }
        }
        public static void CallerWithContinuationTask()
        {
            TraceThreadAndTask("started CallerWithContinuationTask");
            Task<string> t1 = GreetingAsync("Stephanie");
            t1.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);
                TraceThreadAndTask("ended CallerWithContinuationTask");
            });
        }
        public async static void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {s1}{Environment.NewLine} Result 2: {s2}");
        }
        public async static void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            string[] result = await Task.WhenAll(t1, t2);
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {result[0]}{Environment.NewLine} Result 2: {result[1]}");
        }
        public static async ValueTask<string> GreetingValueTaskAsync(string name)
        {
            if (names.TryGetValue(name, out string result))
            {
                return result;
            }
            else
            {
                result = await GreetingAsync(name);
                names.Add(name, result);
                return result;
            }
        }
        public static async void ConvertingAsyncPattern()
        {
            HttpWebRequest request = WebRequest.Create("http://www.microsoft.com") as HttpWebRequest;
            using (WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse(null, null), request.EndGetResponse))
            {
                Stream stream = response.GetResponseStream();
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }
        #endregion

        #region Using ValueTasks
        public static async void UseValueTask()
        {
            string result = await GreetingValueTaskAsync("Katharina");
            Console.WriteLine(result);
            string result2 = await GreetingValueTaskAsync("Katharina");
            Console.WriteLine(result2);
        }
        static ValueTask<string> GreetingValueTask2Async(string name)
        {
            if (names.TryGetValue(name, out string result))
            {
                return new ValueTask<string>(result);
            }
            else
            {
                Task<string> t1 = GreetingAsync(name);
                TaskAwaiter<string> awaiter = t1.GetAwaiter();
                awaiter.OnCompleted(OnCompletion);
                return new ValueTask<string>(t1);
                void OnCompletion()
                {
                    names.Add(name, awaiter.GetResult());
                }
            }
        }
        #endregion

        #region ErrorHandling

        /*
            Asynchronous methods that return void cannot be awaited. The issue
            with this is that exceptions that are thrown from async void methods cannot be
            caught. That’s why it is best to return a Task type from an asynchronous method.
            Handler methods or overridden base methods are exempted from this rule.
        */

        public static async Task ThrowAfter(int ms, string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);
        }
        public static async void DontHandle()
        {
            try
            {
               ThrowAfter(200, "first");
                // exception is not caught because this method is finished
                // before the exception is thrown
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async void HandleOneError()
        {
            try
            {
                await ThrowAfter(2000, "first");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"handled {ex.Message}");
            }
        }

        #endregion

        #region Handling Exceptions with Multiple Asynchronous Methods
        public static async void StartTwoTasks()
        {
            try
            {
                await ThrowAfter(2000, "first");
                await ThrowAfter(1000, "second"); // the second call is not invoked
                                                  // because the first method throws
                                                  // an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine($"handled {ex.Message}");
            }
        }
        public async static void StartTwoTasksParallel()
        {
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await Task.WhenAll(t1, t2);
            }
            catch (Exception ex)
            {
                // just display the exception information of the first task
                // that is awaited within WhenAll
                Console.WriteLine($"handled {ex.Message}");
            }
        }
        public static async void ShowAggregatedException()
        {
            Task taskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await (taskResult = Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"handled {ex.Message}");
                foreach (var ex1 in taskResult.Exception.InnerExceptions)
                {
                    Console.WriteLine($"inner exception {ex1.Message}");
                }
            }
        }
        #endregion
    }
}
