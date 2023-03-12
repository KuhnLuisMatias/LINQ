using System;

namespace Singleton
{
    public sealed class Logger
    {
        private static int i = 0;
        private static Logger logger = null;
        private Logger()
        {
            i++;
            Console.WriteLine($"Instancia Nº: {i}");
        }
        public void ImprimirDetalles(string mensaje) => Console.WriteLine(mensaje);
        public static Logger ObtenerInstancia
        {
            get
            {
                if (logger == null)
                    logger = new Logger();
                return logger;
            }
        }
    }
}
    