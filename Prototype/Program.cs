using FluentInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    internal class Program
    {
        /*
        
        Prototype   
        
        El patrón de diseño de prototipo es un patrón de diseño de software que permite crear objetos a partir de una copia profunda 
        o superficial de objetos existentes. 
        Este patrón se utiliza para reducir el costo de creación de objetos y para hacer que los objetos sean independientes unos de otros.
        En C#, puedes implementar el patrón de diseño de prototipo mediante la implementación de la interfaz ICloneable 
        o mediante la creación de un método Clone en la clase que deseas clonar.
        
        Puntos a recordar: 
        El método MemberwiseClone es parte de la clase System.Object y crea una copia superficial del objeto dado. 
        El método MemberwiseClone sólo copia los campos no estáticos del objeto al nuevo objeto En el proceso de copia, 
        si un campo es de tipo valor, se realiza una copia bit a bit del campo. 
        Si un campo es de tipo referencia, se copia la referencia pero no el objeto referenciado.
        
        */
        static void Main(string[] args)
        {
            var personaOriginal = new Persona();
            personaOriginal.Nombre = "Luis";
            personaOriginal.Apellido = "Kuhn";
            personaOriginal.Edad = "29";
            personaOriginal.Sexo = "Masculino";
            personaOriginal.Direccion = new Direccion() { Calle = "Ejemplo Calle Original" };

            var personaClonada = (Persona) personaOriginal.Clone();
            personaClonada.Edad = "30";

            Console.WriteLine($"Edad Original: {personaOriginal.Edad} \nEdad modificada: {personaClonada.Edad}");

            personaClonada.Direccion.Calle = "Calle modificada";

            Console.WriteLine($"Dirección Original: {personaOriginal.Direccion.Calle} \nDirección modificada: {personaClonada.Direccion.Calle}");

            Console.ReadLine();
        }
    }
}
