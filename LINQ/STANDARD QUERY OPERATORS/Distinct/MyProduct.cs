using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS.Distinct
{
    internal class MyProduct_Distinct : IEquatable<MyProduct_Distinct>
    {
        public string Name { get; set; }
        public int Code { get; set; }   
        public bool Equals(MyProduct_Distinct other)
        {
            if(Object.ReferenceEquals(this, other)) return true;
            if (Object.ReferenceEquals(other, null)) return false;
            return Code.Equals(other.Code) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int hashProductName = Name == null ? 0 : Name.GetHashCode();
            int hashProductCode = Code.GetHashCode();
            return hashProductName ^ hashProductCode;
        }
    }
}
