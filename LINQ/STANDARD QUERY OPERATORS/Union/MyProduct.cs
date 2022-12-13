using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS.Union
{
    internal class MyProduct_Union : IEquatable<MyProduct_Union>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public bool Equals(MyProduct_Union other)
        {
            if (other is null) return false;
            return this.Name == other.Name && this.Code == other.Code;
        }
        public override bool Equals(object obj) => Equals(obj as MyProduct_Union);
        public override int GetHashCode() => (Name, Code).GetHashCode();
    }
}
