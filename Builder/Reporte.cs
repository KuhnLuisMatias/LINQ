using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class Reporte
    {
        public string Tipo { get; set; }
        public string Cabecera { get; set; }
        public string Cuerpo { get; set; }
        public string Pie { get; set; }

        public void ObtenerReporte()
        {
            Console.WriteLine($"Reporte");
            Console.WriteLine($"Tipo: {Tipo}");
            Console.WriteLine($"Cabecera: {Cabecera}");
            Console.WriteLine($"Cuerpo: {Cuerpo}");
            Console.WriteLine($"Pie: {Pie}");
        }
    }
}
