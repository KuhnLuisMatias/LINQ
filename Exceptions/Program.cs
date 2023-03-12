using Exceptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*  C:\Users\Hogar\source\repos\CSharp7\Exceptions\bin\Debug

                CatchBlock.Example();
                CatchBlock.Execute();
                CatchBlock.HandleAll();
             */

            Start();

            Console.Read();
        }

        public static void Start()
        {
            Console.Write("Please type in the name of the file " + "containing the names of the people to be cold called > ");
            string fileName = Console.ReadLine();
            ColdCallFileReaderLoop.Execute(fileName);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
