using System;

namespace Singleton
{
	internal class Program
	{
		/*
			Singleton

			El patrón Singleton es un patrón de diseño de software que garantiza que una clase tenga solo una instancia 
			y proporciona un punto de acceso global a ella. 
			Esto es útil cuando solo se necesita una única instancia de una clase para controlar el estado de la aplicación.

			Ventajas de utilizar Singleton

			* La primera y más importante ventaja de utilizar el patrón de diseño singleton en C# es que se encarga 
			del acceso concurrente al recurso compartido. 
			Esto significa que si estamos compartiendo un recurso con múltiples clientes simultáneamente, 
			entonces el acceso concurrente a ese recurso está bien gestionado por el patrón de diseño singleton. 

			* Puede ser cargado perezosamente y también tiene Inicialización Estática. 
			* Para compartir datos comunes, es decir, datos maestros y datos de configuración que no se cambian con frecuencia en una aplicación. 
			En ese caso, necesitamos almacenar en caché los objetos en la memoria. 
			Como proporciona un único punto global de acceso a una instancia particular, es fácil de mantener. 
			Para reducir la sobrecarga de instanciar un objeto pesado una y otra vez.
		*/
		static void Main(string[] args)
		{
			PrimeraInstancia();
			SegundaInstancia();

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
