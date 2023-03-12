using Reflexion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Reflexion.Model
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true, Inherited = false)]
    public class UltimoAtributoModificado : Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;

        public UltimoAtributoModificado(string dateModified, string changes)
        {
            _dateModified = DateTime.Parse(dateModified);
            _changes = changes;
        }

        public DateTime DateModified => _dateModified;

        public string Changes => _changes;

        public string Issues { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class NovedadAttribute : Attribute
    {
    }
}


