using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    internal static class LocalFunction
    {
        /*
            Unlike normal methods, local functions
            cannot be virtual, abstract, private, or use other modifiers. The only modifiers allowed are async and unsafe.
        */
        public static void IntroWithLambdaExpression()
        {
            Func<int, int, int> add = (x, y) =>
            {
                return x + y;
            };

            int result = add(10, 5);
            Console.WriteLine(result);
        }
        public static void IntroWithLocalFunctions()
        {
            int add(int x, int y)
            {
                return x + y;
            }
            int result = add(10, 5);
            Console.WriteLine(result);
        }
        public static void IntroWithLocalFunctionsWithExpressionBodies()
        {
            int add(int x, int y) => x + y;
            int result = add(37, 5);
            Console.WriteLine(result);
        }
        public static void IntroWithLocalFunctionsWithClosures()
        {
            int z = 3;
            int result = add(37, 5);
            Console.WriteLine(result);

            int add(int x, int y) => x + y + z;
        }

        //Chapter 13. LocalFunctions
        public static void YieldSampleSimple()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleSimple));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };                
                var q = names.Where1(null);
                foreach (var n in q) // callstack position for exception
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }
        public static void YieldSampleWithPrivateMethod()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleWithPrivateMethod));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };
                var q = names.Where2(null); // callstack position for exception
                foreach (var n in q)
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }
        public static void YieldSampleWithPrivateMethod_II()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleWithPrivateMethod));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };
                var q = names.Where3(null); // callstack position for exception
                foreach (var n in q)
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }
        public static IEnumerable<T> Where1<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
        public static IEnumerable<T> Where2<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Where2Impl(source, predicate);
        }
        public static IEnumerable<T> Where2Impl<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
        public static IEnumerable<T> Where3<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        //Recursive Local Functions
        public static void QuickSort<T>(T[] elements) where T : IComparable<T>
        {
            void Sort(int start, int end)
            {
                int i = start, j = end;
                var pivot = elements[(start + end) / 2];
                while (i <= j)
                {
                    while (elements[i].CompareTo(pivot) < 0) i++;
                    while (elements[j].CompareTo(pivot) > 0) j--;
                    if (i <= j)
                    {
                        T tmp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = tmp;
                        i++;
                        j--;
                    }
                }
                if (start < j) Sort(start, j);
                if (i < end) Sort(i, end);
            }
            Sort(0, elements.Length - 1);

            foreach (var element in elements)
                Console.WriteLine(element);
        }
        public static void WhenDoesItEnd()
        {
            Console.WriteLine(nameof(WhenDoesItEnd));
            void InnerLoop(int ix)
            {
                Console.WriteLine(ix++);
                InnerLoop(ix);
            }
            InnerLoop(1);
        }
    }
}
