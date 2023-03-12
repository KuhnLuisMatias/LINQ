using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    internal class Program
    {
        /*
        Patrón decorador
        
        se define una interfaz común para todos los objetos que se puedan decorar (componente), 
        y se implementan clases concretas que representan los componentes básicos. 
        Luego, se definen clases decoradoras que se encargan de agregar funcionalidades adicionales a los componentes básicos. 
        Estas clases decoradoras implementan la misma interfaz que los componentes básicos, 
        por lo que se pueden anidar varias decoraciones unas sobre otras.

        La clave del patrón decorador es que las decoraciones son transparentes para el cliente que usa el objeto decorado. 
        El cliente simplemente trabaja con la interfaz común del objeto y no tiene que preocuparse por las funcionalidades 
        adicionales que se agregan de forma dinámica.
        
        Algunas ventajas del patrón decorador son:
        
        Permite agregar funcionalidades a un objeto de forma flexible y dinámica, sin tener que modificar su estructura original.
        Permite anidar varias decoraciones unas sobre otras, para crear objetos con funcionalidades cada vez más complejas.
        Permite evitar la creación de subclases excesivas para manejar las diferentes combinaciones de funcionalidades.
        
        Algunos casos de uso comunes para el patrón decorador son:
        
        Agregar características a un objeto en tiempo de ejecución, como por ejemplo en la generación de informes o documentos.
        Permitir la personalización de un objeto según las necesidades del usuario.
        Simplificar el mantenimiento de código, al evitar la creación de subclases excesivas para manejar las diferentes 
        combinaciones de funcionalidades.

        */
        static void Main(string[] args)
        {
            IHamburguesa hamburguesa = new HamburguesaClasica();
            hamburguesa = new HamburguesaConHuevo(hamburguesa);

            Console.WriteLine(hamburguesa.ObtenerDescripcion());
            Console.WriteLine(hamburguesa.ObtenerPrecio());
            Console.ReadLine();
        }
    }
}
