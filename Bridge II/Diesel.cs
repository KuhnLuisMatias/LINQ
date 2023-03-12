using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public class Diesel : IMotor
    {
        public void ConsumirCombustible()
        {
            Console.WriteLine("Realizada la explosión de Gasoil");
        }

        public void InyectarCombustible(double cantidad)
        {
            Console.WriteLine($"Inyectando {cantidad} ml3. de Gasoil.");
        }
    }
}
