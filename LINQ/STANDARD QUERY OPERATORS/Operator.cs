using LINQ.Racer;
using LINQ.STANDARD_QUERY_OPERATORS.Distinct;
using LINQ.STANDARD_QUERY_OPERATORS.Union;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Operator
    {
        public static void Distinct()
        {
            MyProduct_Distinct[] products =
            {
                new MyProduct_Distinct { Name = "apple", Code = 9 },
                new MyProduct_Distinct { Name = "orange", Code = 4 },
                new MyProduct_Distinct { Name = "apple", Code = 9 },
                new MyProduct_Distinct { Name = "lemon", Code = 12 }
            };

            IEnumerable<MyProduct_Distinct> noduplicates = products.Distinct();

            foreach (var product in noduplicates)
                Console.WriteLine(product.Name + " " + product.Code);

        }
        public static void Except()
        {
            MyProduct_Distinct[] fruits1 =
            {
                new MyProduct_Distinct { Name = "apple", Code = 9 },
                new MyProduct_Distinct { Name = "orange", Code = 4 },
                new MyProduct_Distinct { Name = "lemon", Code = 12 }
            };

            MyProduct_Distinct[] fruits2 = { new MyProduct_Distinct { Name = "apple", Code = 9 } };

            // Get all the elements from the first array
            // except for the elements from the second array.

            IEnumerable<MyProduct_Distinct> except =
                fruits1.Except(fruits2);

            foreach (var product in except)
                Console.WriteLine(product.Name + " " + product.Code);
        }
        public static void Except_II()
        {
            Product[] fruits1 = { new Product { Name = "apple", Code = 9 },
                       new Product { Name = "orange", Code = 4 },
                        new Product { Name = "lemon", Code = 12 } };

            Product[] fruits2 = { new Product { Name = "apple", Code = 9 } };

            // Get all the elements from the first array
            // except for the elements from the second array.

            IEnumerable<Product> except =
                fruits1.Except(fruits2, new ProductComparer());

            foreach (var product in except)
                Console.WriteLine(product.Name + " " + product.Code);
        }
        public static void Intersect()
        {
            Product[] store1 =
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "orange", Code = 4 }
            };

            Product[] store2 =
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "lemon", Code = 12 }
            };

            // Get the products from the first array
            // that have duplicates in the second array.

            IEnumerable<Product> duplicates =
                store1.Intersect(store2, new ProductComparer());

            foreach (var product in duplicates)
                Console.WriteLine(product.Name + " " + product.Code);
        }
        public static void Intersect_II()
        {

            MyProduct_Distinct[] store1 =
            {
                new MyProduct_Distinct { Name = "apple", Code = 9 },
                new MyProduct_Distinct { Name = "orange", Code = 4 }
            };

            MyProduct_Distinct[] store2 =
            {
                new MyProduct_Distinct { Name = "apple", Code = 9 },
                new MyProduct_Distinct { Name = "lemon", Code = 12 }
            };

            // Get the products from the first array
            // that have duplicates in the second array.

            IEnumerable<MyProduct_Distinct> duplicates =
                store1.Intersect(store2);

            foreach (var product in duplicates)
                Console.WriteLine(product.Name + " " + product.Code);
        }
        public static void Union()
        {
            MyProduct_Union[] store1 =
            {
                new MyProduct_Union { Name = "apple" , Code = 9 },
                new MyProduct_Union { Name = "orange", Code = 4 }
            };

            MyProduct_Union[] store2 =
            {
                new MyProduct_Union { Name = "apple", Code = 9 },
                new MyProduct_Union { Name = "lemon", Code = 12 }
            };

            IEnumerable<MyProduct_Union> union = store1.Union(store2);

            foreach (var product in union)
                Console.WriteLine(product.Name + " " + product.Code);

        }
        public static void Union_II()
        {
            Product[] store10 =
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "orange", Code = 4 }
            };

            Product[] store20 =
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "lemon", Code = 12 }
            };

            //Get the products from the both arrays
            //excluding duplicates.

            IEnumerable<Product> union = store10.Union(store20, new ProductComparer());

            foreach (Product product in union)
                Console.WriteLine(product.Name + " " + product.Code);
        }
        public static void Zip()
        {
            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "one", "two", "three" };
            bool[] values = { true, false, true, false };

            var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);

            var numberosPalabrasBoleanos = numbersAndWords.Zip(values, (first, second) => first + " Valor: " + second);

            foreach (var item in numberosPalabrasBoleanos)
                Console.WriteLine(item);

        }
        public static void Zip_II()
        {
            var racerNames = from r in Formula1.GetChampions()
                             where r.Country == "Italy"
                             orderby r.Wins descending
                             select new
                             {
                                 Name = r.FirstName + " " + r.LastName
                             };

            var racerNamesAndStarts = from r in Formula1.GetChampions()
                                      where r.Country == "Italy"
                                      orderby r.Wins descending
                                      select new
                                      {
                                          r.LastName,
                                          r.Starts
                                      };

            var racers = racerNames.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts: " + second.Starts);
            foreach (var r in racers)
            {
                Console.WriteLine(r);
            }
        }
    }
}
