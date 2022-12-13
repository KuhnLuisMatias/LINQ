using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal class Partition
    {
        static void Take()
        {
            int pageSize = 5;
            int numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() /
            (double)pageSize);
            for (int page = 0; page < numberPages; page++)
            {
                Console.WriteLine($"Page {page}");
                var racers = (from r in Formula1.GetChampions()
                              orderby r.LastName, r.FirstName
                              select r.FirstName + " " + r.LastName).
                Skip(page * pageSize).Take(pageSize);
                foreach (var name in racers)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }
        public static void AggregateCount()
        {
            var query = from r in Formula1.GetChampions()
                        let numberYears = r.Years.Count()
                        where numberYears >= 3
                        orderby numberYears descending, r.LastName
                        select new
                        {
                            Name = r.FirstName + " " + r.LastName,
                            TimesChampion = numberYears
                        };
            foreach (var r in query)
            {
                Console.WriteLine($"{r.Name} {r.TimesChampion}");
            }
        }
        static void AggregateSum()
        {
            var countries = (from c in
                                 from r in Formula1.GetChampions()
                                 group r by r.Country into c
                                 select new
                                 {
                                     Country = c.Key,
                                     Wins = (from r1 in c
                                             select r1.Wins).Sum()
                                 }
                             orderby c.Wins descending, c.Country
                             select c).Take(5);
            foreach (var country in countries)
            {
                Console.WriteLine("{country.Country} {country.Wins}");
            }
        }
        public static void Skip()
        {
            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };

            IEnumerable<int> lowerGrades = grades.OrderByDescending(g => g).Skip(3);

            Console.WriteLine("All grades except the top three are:");
            foreach (int grade in lowerGrades)
                Console.WriteLine(grade);
        }
        public static void TakeWhile()
        {
            /*
                String.Compare
                Menor que cero La subcadena en strA precede a la subcadena en strB en el orden de clasificación. 
                Cero Las subcadenas se encuentran en la misma posición en el orden de clasificación, o el parámetro de longitud es cero. 
                Mayor que cero La subcadena en strA sigue a la subcadena en strB en el orden de clasificación.
             
                TakeWhile
                Devuelve elementos de una secuencia siempre que una condición especificada sea verdadera
             */

            string[] fruits = { "apple", "passionfruit", "banana", "mango", "orange", "blueberry", "grape", "strawberry" };

            IEnumerable<string> result = fruits.TakeWhile(fruit => fruit.Length < 6).ToList();

            foreach (var fruit in result) Console.WriteLine(fruit);

        }

        internal static void SkipWhile()
        {
            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };

            IEnumerable<int> lowerGrades =
                grades
                .OrderByDescending(grade => grade)
                .SkipWhile(grade => grade >= 80);

            Console.WriteLine("All grades below 80:");
            foreach (int grade in lowerGrades)
            {
                Console.WriteLine(grade);
            }
        }
    }
}
