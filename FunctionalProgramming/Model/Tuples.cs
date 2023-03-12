using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming.Model
{
    internal static class Tuples
    {
        /*
         Using tuples, you can avoid declaring method signatures with out parameters.
         out parameters cannot be used with async methods; this restriction does not apply with tuples.
        */
        public static void IntroTuples()
        {
            (string s, int i, Person p) t = ("magic", 42, new Person("Stephanie", "Nagel"));
            Console.WriteLine($"[s]: {t.s}, [i]: {t.i}, [p]: {t.p}");
        }
        public static void IntroTuples_II()
        {
            var t = ("magic", 42, new Person("Matthias", "Nagel"));
            Console.WriteLine($"[string]: {t.Item1}, [int]: {t.Item2},[person]: {t.Item3}");
        }
        public static void IntroTuples_III()
        {
            var t = (s: "magic", i: 42, p: new Person("Matthias", "Nagel"));
            Console.WriteLine($"[s]: {t.s}, [i]: {t.i}, [p]: {t.p}");
        }
        public static void IntroTuples_IV()
        {
            var t2 = (s: "magic", i: 42, p: new Person("Matthias", "Nagel"));

            (string astring, int anumber, Person aperson) t = t2;
            Console.WriteLine($"[s]: {t.astring}, [i]: {t.anumber}, [p]: {t.aperson}");
        }

        //Deconstruction 
        public static void TupleDeconstruction()
        {
            (string s, int i, Person p) = ("magic", 42, new Person("Stephanie", "Nagel"));
            Console.WriteLine($"s: {s}, i: {i}, p: {p}");
        }
        public static void TupleDeconstruction_I()
        {
            (var s1, var i1, var p1) = ("magic", 42, new Person("Stephanie", "Nagel"));
            Console.WriteLine($"s: {s1}, i: {i1}, p: {p1}");

            string s2;
            int i2;
            Person p2;

            (s2, i2, p2) = ("magic", 42, new Person("Katharina", "Nagel"));
            Console.WriteLine($"s: {s2}, i: {i2}, p: {p2}");
        }
        public static void TupleDeconstruction_II()
        {
            (string s3, _, _) = ("magic", 42, new Person("Katharina", "Nagel"));
            Console.WriteLine(s3);
        }

        //Get Tuple
        public static (int result, int remainder) Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remainder = dividend % divisor;
            return (result, remainder);
        }

        public static void BehindTheScenes()
        {
            (string s, int i) t1 = ("magic", 42); // tuple literal
            Console.WriteLine($"{t1.s} {t1.i}");

            ValueTuple<string, int> t2 = ValueTuple.Create("magic", 42);
            Console.WriteLine($"{t2.Item1}, {t2.Item2}");
        }
        public static void UseALibrary()
        {
            var t = SimpleMath.Divide(5, 3);
            Console.WriteLine($"result: {t.result}, remainder: {t.remainder}");
        }
        public static void Mutability()
        {
            // old tuple is a immutable reference type
            Tuple<string, int> t1 = Tuple.Create("old tuple", 42);
            // t1.Item1 = "new string"; // not possible with Tuple

            // new tuple is a mutable value type
            (string s, int i) t2 = ("new tuple", 42);
            t2.s = "new string";
            t2.i = 43;
            t2.i++;
            Console.WriteLine($"new string: {t2.s} int: {t2.i}");
        }

        public static void TupleCompatibility()
        {
            // convert Tuple to ValueTuple
            Tuple<string, int, bool, Person> t1 = Tuple.Create("a string", 42, true, new Person("Katharina", "Nagel"));
            Console.WriteLine($"old tuple - string: {t1.Item1}, number: {t1.Item2},bool: {t1.Item3}, Person: {t1.Item4}");

            (string s, int i, bool b, Person p) t2 = t1.ToValueTuple();
            Console.WriteLine($"new tuple - string: {t2.s}, number: {t2.i}, bool: {t2.b},Person: {t2.p}");

        }
        public static void TupleCompatibility_I()
        {
            Tuple<string, int, bool, Person> t1 = Tuple.Create("a string", 42, true, new Person("Katharina", "Nagel"));
            (string s, int i, bool b, Person p) = t1; // Deconstruct
            Console.WriteLine($"new tuple - string: {s}, number: {i}, bool: {b},Person {p}");
        }
        public static void TupleCompatibility_II()
        {
            Tuple<string, int, bool, Person> t1 = Tuple.Create("a string", 42, true, new Person("Katharina", "Nagel"));
            (string s, int i, bool b, Person p) t2 = t1.ToValueTuple();

            // convert ValueTuple to Tuple
            Tuple<string, int, bool, Person> t3 = t2.ToTuple();
            Console.WriteLine($"old tuple - string: {t1.Item1}, number: {t1.Item2}, " + $"bool: {t1.Item3}, Person: {t1.Item4}");
        }


        public static void TuplesWithLinkedList()
        {
            Console.WriteLine(nameof(TuplesWithLinkedList));
            var list = new LinkedList<int>(Enumerable.Range(0, 10));
            int value;
            LinkedListNode<int> node = list.First;
            do
            {
                (value, node) = (node.Value, node.Next);
                Console.WriteLine(value);
            } while (node != null);
            Console.WriteLine();
        }
        public static void LINQ()
        {
            var racerNamesAndStarts = Formula1.GetChampions()
                                                .Where(r => r.Country == "Italy")
                                                .OrderByDescending(r => r.Wins)
                                                .Select(r => (r.LastName, r.Starts));
            foreach (var r in racerNamesAndStarts)
            {
                Console.WriteLine($"{r.LastName}, starts: {r.Starts}");
            }
        }
    }

    public class SimpleMath
    {
        public static (int result, int remainder) Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remainder = dividend % divisor;
            return (result, remainder);
        }
    }
}
