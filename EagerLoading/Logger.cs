using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagerLoading
{
    public class Logger
    {
        private static int i = 0;
        private static readonly Logger instancia = new Logger();

        private Logger()
        {
            i++;
            Console.WriteLine($"Instancia Nº: {i}");
        }

        public static Logger ObtenerInstancia
        {
            get
            {
                return instancia;
            }
        }

        public void imprimirMensaje(string mensaje) => Console.WriteLine(mensaje);

    }
}
