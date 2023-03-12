using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class SharedFolderProxy : ISharedFolder
    {
        private ISharedFolder _sharedFolder;
        private Employee _employee;

        public SharedFolderProxy(Employee employee)
        {
            this._employee = employee;
        }

        public void performRWOperations()
        {
            if (_employee.Role.ToString() == "ADMIN")
            {
                Console.WriteLine("SharedFolderProxy making call to the real SharedFolder.");
                _sharedFolder = new SharedFolder();
                _sharedFolder.performRWOperations();
            }
            else
                Console.WriteLine("SharedFolderProxy: You don't have permissions to access this folder.");
        }
    }
}
