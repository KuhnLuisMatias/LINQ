using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class ReportePDF : ReporteBuilder
    {
        public override void SetCabecera()
        {
            _reporte.Cabecera = "PDF";
        }

        public override void SetCuerpo()
        {
            _reporte.Cuerpo = "PDF";
        }

        public override void SetPie()
        {
            _reporte.Pie = "PDF";
        }

        public override void SetTipo()
        {
            _reporte.Tipo = "PDF";
        }
    }
}
