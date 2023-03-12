using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public class Auto : Vehiculo
    {
        public Auto(IMotor motor) : base(motor)
        {
        }

        public override void MostrarCaracteristicas()
        {
            Console.WriteLine("Este es un vehículo de tipo Auto con 4 puertas.");
        }
    }
}
