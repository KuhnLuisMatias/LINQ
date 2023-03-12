using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagerLoading
{
    internal class Program
    {
        /*  Common Language Runtime (CLR) 
            
            Es el entorno de tiempo de ejecución de .NET, que es el corazón del framework .NET. 
            Es responsable de administrar la ejecución de código en tiempo de ejecución y proporciona servicios como seguridad, 
            gestión de memoria, verificación de tipos, gestión de excepciones, optimización de código, etc.

            C# es un lenguaje de programación desarrollado por Microsoft que se ejecuta en el CLR. 
            Esto significa que el código C# se compila en un lenguaje intermedio llamado CIL (Common Intermediate Language), 
            que luego se ejecuta en el CLR. 
            
            Esta arquitectura permite que el código C# se ejecute en diferentes plataformas y sistemas operativos que tengan instalado el CLR de .NET,
            lo que hace que sea un lenguaje muy portable.
            
            En resumen, el CLR es el entorno de tiempo de ejecución de .NET y C# es un lenguaje de programación que se ejecuta en el CLR,
            lo que le permite aprovechar todos los servicios y características que ofrece.

         *  EagerLoading 

            La carga ansiosa en el patrón de diseño singleton no es más que un proceso en el que necesitamos inicializar 
            el objeto singleton en el momento del arranque de la aplicación en lugar de hacerlo bajo demanda 
            y mantenerlo listo en memoria para ser utilizado en el futuro. 
            La ventaja de utilizar Eager Loading en el patrón de diseño Singleton es que el CLR (Common Language Runtime) 
            se encargará de la inicialización del objeto y de la seguridad de los hilos. 
            Esto significa que no necesitaremos escribir ningún código explícitamente para manejar la seguridad de los hilos en un entorno multihilo.
         */
        static void Main(string[] args)
        {
            Parallel.Invoke(() => PrimeraInstancia(), () => SegundaInstancia());
            Console.ReadLine();
        }
        public static void PrimeraInstancia()
        {
            var primeraInstancia = Logger.ObtenerInstancia;
            primeraInstancia.imprimirMensaje("Primera");
        }

        public static void SegundaInstancia()
        {
            var segundaInstancia = Logger.ObtenerInstancia;
            segundaInstancia.imprimirMensaje("Segunda");
        }
    }
}
