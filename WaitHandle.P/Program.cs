using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitHandle.P
{
    /*
        WaitHandle es una clase base abstracta en C# que representa un mecanismo de espera para un evento. 
      
        Es utilizado para sincronizar varios subprocesos en una aplicación.
        Un ejemplo de uso de WaitHandle es cuando tienes un subproceso que está realizando una tarea en segundo plano 
        y quieres que otro subproceso espere a que se complete esa tarea antes de continuar. 
        Puedes utilizar WaitHandle para hacer que el subproceso espere hasta que la tarea en segundo plano haya terminado.
    */
    internal class Program
    {
        static AutoResetEvent _autoEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork));
            Console.WriteLine("Doing something else...");
            _autoEvent.WaitOne();
            Console.WriteLine("Work complete");
            Console.ReadLine();
        }

        static void DoWork(object state)
        {
            Console.WriteLine("Working...");
            Thread.Sleep(1000);
            _autoEvent.Set();
        }
    }
}
