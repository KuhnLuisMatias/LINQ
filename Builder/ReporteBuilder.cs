using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public abstract class ReporteBuilder
    {
        protected Reporte _reporte;
        public abstract void SetTipo();
        public abstract void SetCabecera();
        public abstract void SetCuerpo();
        public abstract void SetPie();
        public void CrearReporte()
        {
            _reporte = new Reporte();
        }
        public Reporte ObtenerReporte()
        {
            return _reporte;
        }
    }
}
