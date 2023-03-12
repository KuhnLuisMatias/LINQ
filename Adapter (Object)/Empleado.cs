using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Designacion { get; set; }
        public decimal Sueldo { get; set; }

        public Empleado(int id, string nombre, string designacion, decimal salario)
        {
            ID = id;
            Nombre = nombre;
            Designacion = designacion;
            Sueldo = salario;
        }
    }
}
