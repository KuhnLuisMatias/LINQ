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
                        where numberYears == 1
                        orderby numberYears descending, r.FirstName
                        select new
                        {
                            Pais = r.Country,
                            Ganados = r.Wins,
                            Nombre = r.FirstName + " " + r.LastName,
                            VecesCampeon = numberYears
                        };
            foreach (var r in query)
            {
                Console.WriteLine($"Pais: {r.Pais} Nombre:{r.Nombre} Veces Campeon:{r.VecesCampeon} Ganados: {r.Ganados}");
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
                                Wins = (from r1 in c select r1.Wins).Sum()
                            }
                             orderby c.Wins descending, c.Country select c).Take(5);

            foreach (var country in countries)
            {
                Console.WriteLine($"{country.Country} {country.Wins}");
            }
        }
    }
}
