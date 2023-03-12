using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentInterface
{
    public class Persona //: ICloneable
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public Direccion Direccion { get; set; }
        public Persona Clone()
        {
            Persona persona = (Persona) this.MemberwiseClone();
            persona.Direccion = Direccion.Clone();
            return persona;
        }
    }

    public class Direccion
    {
        public string Calle { get; set; }

        public Direccion Clone()
        {
            return this.MemberwiseClone() as Direccion;
        }
    }
}
