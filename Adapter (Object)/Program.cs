using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class Program
    { /*
            Los patrones de diseño estructurales en C# son patrones de diseño que se centran en cómo se organizan 
            y se relacionan los objetos y clases dentro de un sistema. 
            Estos patrones proporcionan soluciones eficaces y reutilizables para problemas comunes relacionados con 
            la estructura de los sistemas de software.

            ¿Cuándo utilizar Patrones de Diseño Estructurales en C#? 
            
            En aplicaciones en tiempo real, a veces necesitamos cambiar la estructura de una clase o la relación entre las clases 
            pero no queremos que este cambio afecte al proyecto. 
            Por ejemplo, si tenemos dos clases digamos Usuario y Producto. 
            Y la clase Producto se utiliza dentro de la clase Usuario haciendo relaciones uno a muchos entre el Usuario y el Producto. 
            Mañana, la estructura o las relaciones entre estas dos clases cambian. 
            El cliente ahora quiere mantener alejada la clase Producto de la clase Usuario, 
            ya que quiere utilizar la clase Usuario y Producto de forma independiente. 
            Esto es en realidad un cambio estructural y no queremos que este cambio estructural afecte a nuestro proyecto. 
            Aquí es donde el Patrón de Diseño Estructural nos ayuda.

        Adapter

            Este patrón permite que los objetos con interfases incompatibles trabajen juntos. 
            Por ejemplo, se puede crear un adaptador que permita que una clase antigua trabaje con una nueva.
        
        Link: https://dotnettutorials.net/lesson/adapter-design-pattern/ 
        */
        static void Main(string[] args)
        {
            string[,] empleados = new string[5, 4]
            {
                {"101","John","SE","10000"},
                {"102","Smith","SE","20000"},
                {"103","Dev","SSE","30000"},
                {"104","Pam","SE","40000"},
                {"105","Sara","SSE","50000"}
            };

            var target = new Adaptador();
            Console.WriteLine("Comienza el proceso.\n");
            target.ProcesarSueldosCompania(empleados);

            Console.Read();
        }
    }
}
