using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
    public sealed class Logger
    {
        private static int i = 0;
        private static Lazy<Logger> instancia = new Lazy<Logger>(() => new Logger());
        private Logger()
        {
            i++;
            Console.WriteLine($"Intancia Nº : {i}");
        }

        public static Logger ObtenerInstancia
        {
            get { return instancia.Value; }
        }

        public void ImprimirMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}
