using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentInterface
{
    internal class Program
    {
        /*
            FluentInterface

            El patrón de diseño de interfaz fluida es un patrón de diseño de software que 
            permite crear objetos mediante la concatenación de métodos en lugar de utilizar la construcción completa 
            y la asignación de valores a través de constructores y propiedades. 
            Este patrón se utiliza para simplificar la creación de objetos complejos y hacer el código más legible y mantenible.
            
            En C#, puedes implementar el patrón de diseño de interfaz fluida mediante la creación de clases con métodos que devuelven 
            la misma clase y permiten que los métodos se encadenen entre sí.
        
        ¿Cuándo necesitamos utilizar el Patrón de Diseño de Interfaz Fluida en C#? 
            Durante las pruebas UNIT cuando los desarrolladores no son programadores completos. 
            Cuando quiere que su código sea legible por no programadores para que puedan entender si el código satisface su lógica de negocio o no. 
            Si usted es un vendedor de componentes y quiere destacar en el mercado frente a los demás haciendo su interfaz más simple.
        */
        static void Main(string[] args)
        {
            var persona = new FluentBuilder();
            
            persona.Nombre("Luis Matias")
                    .Apellido("Kuhn")
                    .Edad("29")
                    .Sexo("Masculino");

            Console.WriteLine(persona.ToString()); 

            Console.ReadLine();
        }
    }
}
