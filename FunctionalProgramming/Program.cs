using FunctionalProgramming.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace FunctionalProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            
            int[] elements = { 1, 10, 5, 3, 7, 2, 4, 6, 9, 8 };
            
            FunctionalExtensions_Use();

            LocalFunction.IntroWithLambdaExpression();
            LocalFunction.IntroWithLocalFunctions();
            LocalFunction.IntroWithLocalFunctionsWithExpressionBodies();
            LocalFunction.IntroWithLocalFunctionsWithClosures();
            LocalFunction.YieldSampleSimple();
            LocalFunction.YieldSampleWithPrivateMethod();
            LocalFunction.YieldSampleWithPrivateMethod_II();
            LocalFunction.QuickSort(elements);
            LocalFunction.WhenDoesItEnd(); //StackOverflowException
             
            Tuples.IntroTuples();
            Tuples.IntroTuples_II();
            Tuples.IntroTuples_III();
            Tuples.IntroTuples_IV();
            Tuples.TupleDeconstruction();
            Tuples.TupleDeconstruction_I();
            Tuples.TupleDeconstruction_II();
            Tuples.BehindTheScenes();
            Tuples.UseALibrary();
            Tuples.Mutability();
            Tuples.TupleCompatibility();
            Tuples.TupleCompatibility_I();
            Tuples.TupleCompatibility_II();
            Tuples.TuplesWithLinkedList();
            Tuples.LINQ();

            (int result, int remainder) t = Tuples.Divide(20, 4);
            Console.WriteLine($"Result: {t.result} Remainder:{t.remainder}");
            
            RacerExtensions.DeconstructWithExtensionsMethods();
            */

            PatternMatching();

            ReadKey();
        }

        public static void FunctionalExtensions_Use()
        {
            //Example 1
            Func<int, int> f1 = x => x + 1;
            Func<int, int> f2 = x => x + 2;
            Func<int, int> f3 = FunctionalExtensions.Compose(f1, f2);
            var x1 = f3(39);
            WriteLine(x1);

            //Example 2
            Func<string, Person> p1 = name => new Person(name);
            Func<Person, string> p2 = obj => $"Hello, {obj.FirstName}";
            var greetPerson = FunctionalExtensions.Compose(p1, p2);
            WriteLine(greetPerson("Leonel Messi"));
        }

        public static void PatternMatching()
        {
            var p1 = new Person("Katharina", "Nagel");
            var p2 = new Person("Matthias", "Nagel");
            var p3 = new Person("Stephanie", "Nagel");
            object[] data = { null, 42, "astring", p1, new Person[] { p2, p3 } };
            foreach (var item in data)
            {
                IsOperator(item);
            }
            foreach (var item in data)
            {
                SwitchStatement(item);
            }

        }

        public static void IsOperator(object item)
        {
            // const pattern
            if (item is null)
            {
                Console.WriteLine("item is null");
            }
            
            if (item is int i)
            {
                Console.WriteLine($"Item is of type int with the value {i}");
            }
           
            if (item is string s)
            {
                Console.WriteLine($"Item is a string: {s}");
            }
           
            if (item is Person p && p.FirstName.StartsWith("Ka"))
            {
                Console.WriteLine($"Item is a person: {p.FirstName} {p.LastName}");
            }
            
            if (item is IEnumerable<Person> people)
            {
                string names = string.Join(", ",people.Select(p1 => p1.FirstName).ToArray());
                Console.WriteLine($"it's a Person collection containing {names}");
            }

            // var pattern
            if (item is var every)
            {
                Console.WriteLine($"it's var of type {every?.GetType().Name ?? "null"} " + $"with the value {every ?? "nothing"}");
            }
        }

        public static void SwitchStatement(object item)
        {
            switch (item)
            {
                case null:
                case 42:
                    Console.WriteLine("it's a const pattern");
                    break;
                case int i:
                    Console.WriteLine($"it's a type pattern with int: {i}");
                    break;
                case string s:
                    Console.WriteLine($"it's a type pattern with string: {s}");
                    break;
                case Person p when p.FirstName == "Katharina":
                    Console.WriteLine($"type pattern match with Person and " +
                    $"when clause: {p}");
                    break;
                case Person p:
                    Console.WriteLine($"type pattern match with Person: {p}");
                    break;
                case var every:
                    Console.WriteLine($"var pattern match: {every?.GetType().Name}");
                    break;
                default:
            }
        }

        
    }
}
