using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    internal class Program
    {
        /* Patrón Proxy 
        
        Se utiliza cuando se desea controlar el acceso a un objeto o protegerlo de solicitudes incorrectas o malintencionadas. 
        Aquí hay algunas situaciones comunes en las que se utiliza el patrón Proxy:
        
        Control de acceso: 
                           El patrón Proxy se utiliza para controlar el acceso a objetos sensibles,
                           como cuentas bancarias, bases de datos, sistemas de archivos, etc. 
                           El Proxy actúa como un intermediario y solo permite el acceso a los objetos por parte de aquellos 
                           que cumplen con ciertos criterios.

        Carga diferida: 
                           El patrón Proxy se utiliza para cargar objetos costosos en recursos solo cuando se necesitan. 
                           Esto ayuda a mejorar el rendimiento y la eficiencia del sistema,
                           ya que los objetos no se cargan hasta que se necesiten.

        Comunicación remota: 
                           
                           El patrón Proxy se utiliza para proporcionar un objeto local que se comporta como un objeto remoto. 
                           Esto se utiliza comúnmente en aplicaciones de red para comunicarse con servidores remotos o servicios web.

        Control de transacciones: 
                           
                           El patrón Proxy se utiliza para controlar transacciones complejas y mantener la integridad de los datos. 
                           El Proxy actúa como un intermediario y coordina las transacciones entre los objetos involucrados.
        
        En resumen, el patrón Proxy se utiliza para agregar una capa adicional de control y seguridad al acceso a objetos. 
        Se puede utilizar en cualquier situación en la que se necesite un intermediario para controlar las solicitudes 
        a un objeto y asegurarse de que se manejen de manera adecuada. */

        static void Main(string[] args)
        {
            var empleado = new Employee("Matias", "ADMIN");
            var sharedFolderProxy = new SharedFolderProxy(empleado);
            sharedFolderProxy.performRWOperations();

            Console.WriteLine(Environment.NewLine);
            var empleado2 = new Employee("Test", "Test");
            sharedFolderProxy = new SharedFolderProxy(empleado2);
            sharedFolderProxy.performRWOperations();

            Console.ReadLine();
        }
    }
}
