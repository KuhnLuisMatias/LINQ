using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Aggregate
    {
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
        public static void AggregateSum()
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
                Console.WriteLine($"{country.Country} {country.Wins}");
            }
        }
    }
}
