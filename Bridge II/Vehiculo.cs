using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_II
{
    public abstract class Vehiculo
    {
        private IMotor _motor;

        public Vehiculo(IMotor motor)
        {
            _motor = motor;
        }
        
        public void Acelerar(double combustible)
        {
            _motor.InyectarCombustible(combustible);
            _motor.ConsumirCombustible();
        }

        public abstract void MostrarCaracteristicas();
    }
}
