using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class CompositeClass : IComponent
    {
        public string Name { get; set; }
        public List<IComponent> components = new List<IComponent>();
        public CompositeClass(string name)
        {
            Name = name;
        }
        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }
        public void DisplayPrice()
        {
            Console.WriteLine(Name);
            foreach (var component in components)
            {
                component.DisplayPrice();
            }
        }
    }
}
