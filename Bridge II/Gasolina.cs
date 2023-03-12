using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public class Gasolina : IMotor
    {
        public void ConsumirCombustible()
        {
            Console.WriteLine("Realizando combustión de la gasolina.");
        }

        public void InyectarCombustible(double cantidad)
        {
            Console.WriteLine($"Inyectando {cantidad} ml3 de Gasolina");
        }
    }
}
