using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IComponent cpu = new Leaf("Intel Core 2 Duo", 1000);
            IComponent ramMemory = new Leaf("Memoria Ram", 500);
            CompositeClass motherBoard = new CompositeClass("MotherBoard");

            motherBoard.AddComponent(cpu);
            motherBoard.AddComponent(ramMemory);
            motherBoard.DisplayPrice();

            Console.ReadLine();
        }
    }
}
