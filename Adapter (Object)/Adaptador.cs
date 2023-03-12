using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class Adaptador : ITarget
    {
        SistemaDeFacturacionDeTerceros sistema = new SistemaDeFacturacionDeTerceros();

        public void ProcesarSueldosCompania(string[,] arrayEmpleados)
        {

            int id = 0;
            string nombre = null;
            string designacion = null;
            decimal sueldo = 0M;

            List<Empleado> listaEmpleados = new List<Empleado>();

            for (int i = 0; i < arrayEmpleados.GetLength(0); i++)
            {
                for (int j = 0; j < arrayEmpleados.GetLength(1); j++)
                {
                    switch (j)
                    {
                        case 0:
                            id = int.Parse(arrayEmpleados[i, j]);
                            break;
                        case 1:
                            nombre = arrayEmpleados[i, j];
                            break;
                        case 2:
                            designacion = arrayEmpleados[i, j];
                            break;
                        default:
                            sueldo = Decimal.Parse(arrayEmpleados[i, j]);
                            break;
                    }
                }
                listaEmpleados.Add(new Empleado(id, nombre, designacion, sueldo));
            }
            Console.WriteLine("Adaptador convirtio Array de empleados en una Lista.");
            Console.WriteLine("Se inicia el proceso de los sueldos de los empleados...");
            sistema.ProcesarSueldo(listaEmpleados);
        }
    }
}
