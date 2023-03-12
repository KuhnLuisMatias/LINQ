using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Composite
{
    public class Leaf : IComponent
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Leaf(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public void DisplayPrice()
        {
            Console.WriteLine($"El precio del componente {Name} es ${Price}");
        }
    }
}
