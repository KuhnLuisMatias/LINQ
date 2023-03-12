using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafe
{
    public sealed class Logger
    {
        public static int i = 0;
        private static Logger instancia;
        private static readonly object Instancelock = new object();

        public Logger()
        {
            i++;
            Console.WriteLine($"Instancia Nº: {i}");
        }
        
        public void ImprimirDetalles(string mensaje) => Console.WriteLine(mensaje);
        
        public static Logger ObtenerInstancia
        {
            get
            {
                if (instancia == null)
                {
                    lock (Instancelock)
                    {
                        if (instancia == null)
                        {
                            instancia = new Logger();
                        }
                    }
                }
                /*
               if (instancia == null)
               {
                   Interlocked.CompareExchange(ref instancia, new Logger(), null);

                   Interlocked.CompareExchange es un método estático de la clase Interlocked que se utiliza para realizar una operación de asignación 
                   de forma segura en un entorno multihilo. 

                   Funciona de la siguiente manera:

                   Toma dos argumentos: la variable que se va a asignar (1ra Arg) y el valor que se va a comparar (3er Arg) con la variable actual.
                   Compara la variable actual con el valor comparado.
                   Si son iguales, se realiza la asignación (compareInstance a intancia) y se devuelve el valor anterior de la variable (null).
                   Si no son iguales, se devuelve el valor actual de la variable sin realizar la asignación.

                   El método es seguro para el acceso concurrente, ya que utiliza un mecanismo de bloqueo interno para garantizar 
                   que solo un hilo pueda realizar la asignación en cualquier momento. 
                   De esta manera, se evita la posibilidad de que varios hilos asignen valores a la misma variable al mismo tiempo, 
                   lo que resultaría en un comportamiento incorrecto. 
                    
                }
                */
                return instancia;
            }
        }
    }
}