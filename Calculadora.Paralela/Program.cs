using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calculadora.Paralela
{
    internal class Program
    {

        /*
        ManualResetEventSlim es un tipo de evento sincrónico que permite a un hilo esperar a que otro hilo establezca el evento. 
        Es similar a ManualResetEvent, pero con una implementación más ligera y optimizada para escenarios de alto rendimiento.
        El comportamiento de ManualResetEventSlim se divide en dos estados: "set" y "reset". 
        Cuando se crea, el evento se encuentra en un estado "reset" 
        y cualquier hilo que llame al método Wait() se bloqueará hasta que el evento se establezca. 
        Una vez establecido, cualquier hilo que llame al método Wait() continuará inmediatamente.
        ManualResetEventSlim se utiliza a menudo para sincronizar varios hilos de trabajo. 
        Por ejemplo, puede utilizarse para garantizar que un hilo de trabajo no comience hasta que otro hilo haya terminado una tarea específica. 
        También puede utilizarse para garantizar que varios hilos de trabajo se ejecuten en un orden específico 
        o para evitar que varios hilos accedan a un recurso compartido al mismo tiempo.
        
        */
        static void Main(string[] args)
        {
            /*
            Un ManualResetEventSlim es un tipo de objeto de sincronización 
            que permite a uno o más hilos esperar hasta que se establezca una señal. 
            En este caso, al inicializarlo con "false", significa que los hilos que esperen en él no serán notificados inmediatamente, 
            sino hasta que alguien llame al método "Set()" en el objeto ManualResetEventSlim.
            */
            const int tareas = 4;
            var mEvents = new ManualResetEventSlim[tareas];
            var waitHandles = new WaitHandle[tareas];
            var calcs = new Calculator[tareas];
            for (int i = 0; i < tareas; i++)
            {
                int j = i;
                mEvents[i] = new ManualResetEventSlim(false);
                waitHandles[i] = mEvents[i].WaitHandle;
                calcs[i] = new Calculator(mEvents[i]);
                Task.Run(() => calcs[j].Calculation(j + 1, j + 3));
            }

            for (int i = 0; i < tareas; i++)
            {
                int index = WaitHandle.WaitAny(waitHandles);
                if (index == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("Timeout!!");
                }
                else
                {
                    mEvents[index].Reset();
                    Console.WriteLine($"finished task for {index}, result: {calcs[index].Result}");
                }
            }
            /*
            mEvents[i].WaitHandle es una propiedad de la clase ManualResetEventSlim 
            que devuelve un objeto WaitHandle asociado al evento ManualResetEventSlim actual. 
            El objeto WaitHandle representa un objeto de espera que se puede utilizar para esperar que el evento ManualResetEventSlim sea señalado.
            En otras palabras, mEvents[i].WaitHandle es un objeto que permite esperar (bloquear) hasta que el evento mEvents[i] sea señalado.
            */

            Console.ReadKey();  
        }
    }

    public class Calculator
    {
        private ManualResetEventSlim _mEvent;
        public int Result { get; private set; }
        public Calculator(ManualResetEventSlim ev)
        {
            _mEvent = ev;
        }
        public void Calculation(int x, int y)
        {
            Console.WriteLine($"Task {Task.CurrentId} starts calculation");
            Task.Delay(new Random().Next(3000)).Wait();
            Result = x + y;
            // signal the event-completed!
            Console.WriteLine($"Task {Task.CurrentId} is ready");
            _mEvent.Set();
        }
    }
}
