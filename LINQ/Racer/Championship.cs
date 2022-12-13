using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Racer
{
    public class Championship
    {
        public Championship(int year, string first, string second, string third)
        {
            Year = year;
            First = first;
            Second = second;
            Third = third;
        }
        public int Year { get; }
        public string First { get; }
        public string Second { get; }
        public string Third { get; }


    }
}
