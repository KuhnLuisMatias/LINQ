using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => PrimeraInstancia(), 
                () => SegundaInstancia());

            Console.ReadLine();
        }

        public static void PrimeraInstancia()
        {
            var primeraInstancia = Logger.ObtenerInstancia;
            primeraInstancia.ImprimirMensaje("Primera");
        }

        public static void SegundaInstancia()
        {
            var segundaInstancia = Logger.ObtenerInstancia;
            segundaInstancia.ImprimirMensaje("Segunda");
        }
    }
}
