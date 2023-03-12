using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    internal class Program
    {
        /*
            Builder

            El patrón de diseño Builder es un patrón de creación que se utiliza para construir objetos complejos paso a paso. 
            Este patrón es útil cuando la construcción de un objeto es demasiado complicada 
            y requiere la configuración de diferentes propiedades y atributos.

            Fuente: https://dotnettutorials.net/lesson/builder-design-pattern/

        ReportBuilder: El ReportBuilder es una interfaz que define todos los pasos que se utilizan para fabricar el producto concreto. 
        
        Builder concreto: La clase ConcreteBuilder implementa la interfaz Builder y proporciona la implementación 
        de todos los métodos abstractos. (Excel y PDF)
        El Concrete Builder es responsable de construir y ensamblar las partes individuales del producto implementando la interfaz Builder. 
        También define y rastrea la representación que crea. 

        Director: El Director toma esos procesos individuales del Constructor y define la secuencia para construir el producto. 

        Producto: El Producto es una clase y queremos crear este objeto producto utilizando el patrón de diseño constructor. 
        Esta clase define diferentes partes que harán el producto

        */
        static void Main(string[] args)
        {
            Reporte reporte;
            ReporteDirector reporteDirector = new ReporteDirector();
            ReporteExcel reporteExcel = new ReporteExcel();
            ReportePDF reportePDF = new ReportePDF();

            reporte = reporteDirector.GenerarReporte(reportePDF);
            reporte.ObtenerReporte();

            Console.WriteLine(Environment.NewLine); 
            reporte = reporteDirector.GenerarReporte(reporteExcel);
            reporte.ObtenerReporte();


            Console.ReadKey();
        }


    }

}
