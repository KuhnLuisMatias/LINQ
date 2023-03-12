using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    internal class HamburguesaClasica : IHamburguesa
    {
        public string ObtenerDescripcion()
        {
            return "Hamburguesa clásica";
        }

        public double ObtenerPrecio()
        {
            return 5.00;
        }
    }
}
