using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class Orden // Clase fachada
    { 
        public void RealizarPedido()
        {
            Producto producto = new Producto();
            Pago pago = new Pago();
            Recibo recibo = new Recibo();

            producto.ObtenerDetalles();
            pago.RealizarPago();
            recibo.EnviarRecibo();
        }
    }
}
