using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Sorting
    {
        public static void SortDescending()
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Country == "Italy"
                         orderby r.Wins descending
                         select r;

            foreach (var racer in racers) Console.WriteLine($"Apellido: {racer.LastName} \nNombre: {racer.FirstName} \nGanados: {racer.Wins} \n ---");
        }
        public static void SortDescendingWithMethods()
        {
            var racers = Formula1.GetChampions()
                        .Where(r => r.Country == "Italy")
                        .OrderByDescending(r => r.Wins)
                        .Select(r => r);

            foreach (var racer in racers) Console.WriteLine($"Apellido: {racer.LastName} \nNombre: {racer.FirstName} \nGanados: {racer.Wins} \n ---");
        }
        public static void SortMultiple()
        {
            var racers = (from r in Formula1.GetChampions()
                          orderby r.Country, r.LastName, r.FirstName
                          select r).Take(10);

            foreach (var racer in racers) Console.WriteLine($"Apellido: {racer.LastName} \nNombre: {racer.FirstName} \nGanados: {racer.Wins} \n ---");
        }
        public static void SortMultipleWithMethods()
        {
            var racers = Formula1.GetChampions()
                        .OrderBy(r => r.Country)
                        .ThenBy(r => r.LastName)
                        .ThenBy(r => r.FirstName)
                        .Take(10);

            foreach (var racer in racers) Console.WriteLine($"Apellido: {racer.LastName} \nNombre: {racer.FirstName} \nGanados: {racer.Wins} \n ---");
        }
    }
}
