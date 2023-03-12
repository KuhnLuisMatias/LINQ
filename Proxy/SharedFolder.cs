using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class SharedFolder : ISharedFolder
    {
        public void performRWOperations()
        {
            Console.WriteLine("Performing Read Write operations on the SharedFolder.");
        }
    }
}
