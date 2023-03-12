using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FluentInterface
{
    public class FluentBuilder
    {
        private Persona fluentPersona = new Persona();

        public FluentBuilder Nombre(string nombre)
        {
            fluentPersona.Nombre = nombre;
            return this;
        }
        public FluentBuilder Apellido(string apellido)
        {
            fluentPersona.Apellido = apellido;
            return this;
        }
        public FluentBuilder Sexo(string sexo)
        {
            fluentPersona.Sexo = sexo;
            return this;
        }
        public FluentBuilder Edad(string edad)
        {
            fluentPersona.Edad = edad;
            return this;
        }

        public override string ToString()
        {
            return $"Nombre: {fluentPersona.Nombre},Apellido: {fluentPersona.Apellido}, Edad: {fluentPersona.Edad}, Sexo: {fluentPersona.Sexo}";
        }
    }
}
