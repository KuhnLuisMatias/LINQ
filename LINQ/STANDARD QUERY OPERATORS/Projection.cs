using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Projection
    {
        public static void CompoundFrom()
        {
            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select r.FirstName + " " + r.LastName;

            foreach (var driver in ferrariDrivers) Console.WriteLine(driver);
        }
        public static void CompoundFromWithMethods()
        {
            /*El metodo SelectMany puede utilizarse para iterar una secuencia de una secuencia
             
                IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>
             */

            var ferrariDrivers = Formula1.GetChampions()
                                .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
                                .Where(r => r.Car == "Ferrari")
                                .OrderBy(r => r.Racer.LastName)
                                .Select(r => r.Racer.FirstName + " " + r.Racer.LastName);

            foreach (var driver in ferrariDrivers) Console.WriteLine(driver);
        }
    }
}
