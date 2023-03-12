using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public Employee(string name,string role)
        {
            this.Name = name;
            this.Role = role;   
        }
    }
}
