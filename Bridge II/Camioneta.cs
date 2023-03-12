using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public class Camioneta : Vehiculo
    {
        public Camioneta(IMotor motor) : base(motor)
        {
        }

        public override void MostrarCaracteristicas()
        {
            Console.WriteLine("Este vehículo es una pickup 4x4.");
        }
    }
}
