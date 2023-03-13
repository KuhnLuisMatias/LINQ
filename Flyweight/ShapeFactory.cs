using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{
    public class ShapeFactory
    {
        private static Dictionary<string, Shape> _shapeMap = new Dictionary<string, Shape>();
        public static Shape GetShape(string shapeType)
        {
            Shape shape = null;
            if (shapeType.Equals("circle", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!_shapeMap.TryGetValue("circle", out shape))
                {
                    shape = new Circle();
                    _shapeMap.Add("circle", shape);
                    Console.WriteLine("Creating circle object with out any color");
                }
            }

            return shape;
        }
    }
}
