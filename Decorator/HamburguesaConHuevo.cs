using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    public class HamburguesaConHuevo : HamburguesaDecorador
    {
        public HamburguesaConHuevo(IHamburguesa hamburguesa) : base(hamburguesa)
        {
        }

        public override string ObtenerDescripcion()
        {
            return _hamburguesa.ObtenerDescripcion() + " + Huevo";
        }

        public override double ObtenerPrecio()
        {
            return _hamburguesa.ObtenerPrecio() + 2.25;
        }
    }
}
