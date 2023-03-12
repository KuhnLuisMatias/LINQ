using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public class Program
    {
        static void Main(string[] args)
        {
            Vehiculo automovil = new Auto(new Gasolina());
            Vehiculo camioneta = new Camioneta(new Diesel());
            
            automovil.Acelerar(500);
            automovil.MostrarCaracteristicas();

            camioneta.Acelerar(1000);
            camioneta.MostrarCaracteristicas();

            Console.ReadLine();
        }
    }
}
