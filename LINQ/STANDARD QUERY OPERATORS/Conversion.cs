using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Conversion
    {
        public static void ToDictionary()
        {
            var packages = new List<Package>
            {
                new Package { Company = "MercadoLibre",Weight = 15,TrackingNumber= 01},
                new Package { Company = "Adreani",Weight = 30,TrackingNumber= 02},
                new Package { Company = "Correo Argentino",Weight = 45,TrackingNumber= 03},
            };

            Dictionary<long,Package> dictionary = packages.ToDictionary(package=>package.TrackingNumber);

            foreach (var package in dictionary) 
                Console.WriteLine($"Numero de seguimiento: {package.Key}, Compañia: {package.Value.Company}, Peso: {package.Value.Weight} (Kg)");
        }

        public static void ToList()
        {
            List<Racer.Racer> racers = (from r in Formula1.GetChampions()
                                        where r.Starts > 200
                                        orderby r.Starts descending
                                        select r).ToList();
            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer} {racer:S}");
            }
        }
    }

    class Package
    {
        public string Company { get; set; }
        public double Weight { get; set; }
        public long TrackingNumber { get; set; }
    }
}
