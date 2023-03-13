using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{
    public class Circle : Shape
    {
        public string Color { get; set; }
        public void Draw()
        {
            Console.WriteLine($"Shape circle color: {Color}");
        }
    }
}
