using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafe
{
    internal class Program
    {
        /*
            Singleton en un entorno Multihilo utilizamos: Interlocked.CompareExchange 
            
            Interlocked.CompareExchange es un método estático de la clase Interlocked que se utiliza para realizar una operación de asignación 
            de forma segura en un entorno multihilo. 
        
            Funciona de la siguiente manera:

            Toma dos argumentos: la variable que se va a asignar y el valor que se va a comparar con la variable actual.
            Compara la variable actual con el valor comparado.
            Si son iguales, se realiza la asignación y se devuelve el valor anterior de la variable.
            Si no son iguales, se devuelve el valor actual de la variable sin realizar la asignación.
            El método es seguro para el acceso concurrente, ya que utiliza un mecanismo de bloqueo interno para garantizar 
            que solo un hilo pueda realizar la asignación en cualquier momento. 
            De esta manera, se evita la posibilidad de que varios hilos asignen valores a la misma variable al mismo tiempo, 
            lo que resultaría en un comportamiento incorrecto. 
         */
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => PrimeraInstancia(),
                () => SegundaInstancia()
                );

            Console.ReadLine();
        }

        private static void PrimeraInstancia()
        {
            var logger = Logger.ObtenerInstancia;
            logger.ImprimirDetalles("Instancia 1");
        }

        private static void SegundaInstancia()
        {
            var logger = Logger.ObtenerInstancia;
            logger.ImprimirDetalles("Instancia 2");
        }
    }
}
