using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class ReporteDirector
    {
        public Reporte GenerarReporte(ReporteBuilder reporteBuilder)
        {
            reporteBuilder.CrearReporte();
            reporteBuilder.SetTipo();
            reporteBuilder.SetCabecera();
            reporteBuilder.SetCuerpo();
            reporteBuilder.SetPie();

            return reporteBuilder.ObtenerReporte();
        }
    }
}
