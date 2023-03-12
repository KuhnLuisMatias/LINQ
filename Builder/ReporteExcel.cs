using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class ReporteExcel : ReporteBuilder
    {
        public override void SetCabecera()
        {
            _reporte.Cabecera = "Excel";
        }

        public override void SetCuerpo()
        {
            _reporte.Cuerpo = "Excel";
        }

        public override void SetPie()
        {
            _reporte.Pie = "Excel";
        }

        public override void SetTipo()
        {
            _reporte.Tipo = "Excel";
        }
    }
}
