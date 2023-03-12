using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class SistemaDeFacturacionDeTerceros
    {
        //Esta clase acepta información de empleados como una lista para así lograr procesar acada salario de cada uno de los empleados.

        public void ProcesarSueldo(List<Empleado> listaEmpleado)
        {
            foreach(Empleado empleado in listaEmpleado)
            {
                Console.WriteLine($"Empleado: {empleado.Nombre}, Sueldo: {empleado.Sueldo}");
            }
        }
    }
}
