using Reflexion.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflexion
{
    internal class Program
    {
        private static readonly StringBuilder textoSalida = new StringBuilder(1000);
        private static DateTime fechaDesde = new DateTime(2010, 2, 1);
        static void Main(string[] args)
        {
            //EjemploReflexion_I();            
            EjemploReflexion_II();
            
            Console.ReadLine();
        }
        static void EjemploReflexion_I()
        {

            Type[] tiposDatos = 
            { 
                typeof(int), 
                typeof(Vector),
                typeof(UltimoAtributoModificado)
                //etc
            };

            foreach (var dato in tiposDatos)
            {
                MostrarMensaje($"Tipo de dato [{dato.Name}]\n");
                AnalizarTipo(dato);
                MostrarMensaje("______________________________\n");
            }
            
            Console.WriteLine(textoSalida.ToString());
        }
        static void EjemploReflexion_II()
        {
            Assembly ensamblado = Assembly.Load(new AssemblyName("Reflexion"));
            Attribute soporteAtributo = ensamblado.GetCustomAttribute(typeof(NovedadAttribute));

            MostrarMensaje($"Ensamblado: {ensamblado.FullName}");

            if (soporteAtributo == null)
            {
                MostrarMensaje("Este ensamblado no soporta atributos Novedad");
                return;
            }
            else
            {
                MostrarMensaje("Tipos definidos:");
            }

            //    La propiedad Assembly.ExportedTypes se utiliza para obtener una colección de todos los tipos definidos en este ensamblado y,
            //    a continuación, se realiza un bucle a través de ellos.
            //    Para cada uno, llama a un método, MostarInformacionDelTipoDeDato, que añade el texto relevante, 
            //    incluyendo detalles sobre cualquier instancia de UltimoAtributoModificado, al campo textoSalida. 

            foreach (Type tipoDefinido in ensamblado.ExportedTypes)
            {
                MostarInformacionDelTipoDeDato(tipoDefinido);
            }

            Console.WriteLine($"Novedades a partir de la fecha {fechaDesde:D}");
            Console.WriteLine(textoSalida.ToString());
        }
        static void AnalizarTipo(Type tipo)
        {
            TypeInfo infoTipo = tipo.GetTypeInfo();
            Type tipoBase = infoTipo.BaseType;

            MostrarMensaje($"Namespace: \t{tipo.Namespace}");
            MostrarMensaje($"Full Name: \t{tipo.FullName}");
            MostrarMensaje($"Es Array: \t{tipo.IsArray}");
            MostrarMensaje($"Es Clase: \t{tipo.IsClass}");
            MostrarMensaje($"Es Enum: \t{tipo.IsEnum}");
            MostrarMensaje($"Es Primitivo: \t{tipo.IsPrimitive}");
            MostrarMensaje($"Es ValueType: \t{tipo.IsValueType}");

            if (tipoBase != null)
                MostrarMensaje($"Tipo Base: \t{tipoBase.Name}");
                        
            MostrarMensaje("\n Miembros Publicos:\n");
            
            foreach (MemberInfo member in tipo.GetMembers())
                MostrarMensaje($"{member.DeclaringType} {member.MemberType} {member.Name}");
            
            MostrarMensaje("");
        }
        static void MostrarMensaje(string Text) => textoSalida.Append($"{Environment.NewLine} {Text}");
        private static void MostarInformacionDelTipoDeDato(Type type)
        {
            if (!type.GetTypeInfo().IsClass)
            {
                return;
            }

            MostrarMensaje($"{Environment.NewLine}Clase {type.Name}");

            IEnumerable<UltimoAtributoModificado> ultimasModificaciones = type.GetTypeInfo()
                                                                              .GetCustomAttributes()
                                                                              .OfType<UltimoAtributoModificado>()
                                                                              .Where(a => a.DateModified >= fechaDesde)
                                                                              .ToArray();
            
            if (ultimasModificaciones.Count() == 0)
            {
                MostrarMensaje($"\tNo hubo cambios en la clase {type.Name}{Environment.NewLine}");
            }
            else
            {
                foreach (UltimoAtributoModificado atributo in ultimasModificaciones)
                {
                    EscribirInformacionAttribute(atributo);
                }
            }

            MostrarMensaje("Cambios en los métodos de esta clase:");

            foreach (MethodInfo method in type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
            {
                
                IEnumerable<UltimoAtributoModificado> attributesToMethods = method.GetCustomAttributes()
                                                                                  .OfType<UltimoAtributoModificado>()
                                                                                  .Where(a => a.DateModified >= fechaDesde)
                                                                                  .ToArray();

                if (attributesToMethods.Count() > 0)
                {
                    MostrarMensaje($"{method.ReturnType} {method.Name}()");

                    foreach (Attribute attribute in attributesToMethods)
                    {
                        EscribirInformacionAttribute(attribute);
                    }
                }
            }
        }
        private static void EscribirInformacionAttribute(Attribute attribute)
        {
            if (attribute is UltimoAtributoModificado lastModifiedAttribute)
            {
                MostrarMensaje($"\tModificado: {lastModifiedAttribute.DateModified:D}: {lastModifiedAttribute.Changes}");

                if (lastModifiedAttribute.Issues != null)
                {
                    MostrarMensaje($"\tIssues Pendientes: {lastModifiedAttribute.Issues}");
                }
            }
        }
    }
}
