using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class Program
    {
        /*
        Patrón Fachada 
            
        Es un patrón de arquitectura que proporciona una interfaz simple para un sistema complejo. 
        El objetivo principal es ocultar la complejidad de un sistema detrás de una interfaz sencilla y unificada.
      
        Hay dos clases involucradas en el patrón de diseño de fachada. Son las siguientes:

        * La clase Fachada sabe qué clases del subsistema son responsables de una determinada petición 
        y entonces delega las peticiones del cliente a los objetos del subsistema apropiados. 
        
        * Las clases del Subsistema Implementan sus respectivas funcionalidades asignadas a ellos 
        y estos subsistemas no tienen ningún conocimiento de la fachada.

        Necesitamos utilizar el patrón de diseño Facade en C# cuando:

        Queremos proporcionar una interfaz sencilla a un subsistema complejo. 
        Los subsistemas a menudo se vuelven más complejos a medida que evolucionan. 
        Hay muchas dependencias entre los clientes y las clases de implementación
         */
        static void Main(string[] args)
        {
            var orden = new Orden();
            orden.RealizarPedido();

            Console.ReadLine();
        }
    }
}
