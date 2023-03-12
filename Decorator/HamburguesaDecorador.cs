using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    public abstract class HamburguesaDecorador : IHamburguesa
    {
        protected IHamburguesa _hamburguesa;
        public HamburguesaDecorador(IHamburguesa hamburguesa) 
        { 
            _hamburguesa = hamburguesa;
        }
        public virtual string ObtenerDescripcion()
        {
            return "Nada";
        }

        public virtual double ObtenerPrecio()
        {
            return 0;
        }
    }
}
