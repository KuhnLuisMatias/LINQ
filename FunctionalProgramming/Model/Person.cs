using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming.Model
{
    public class Person
    {
        public Person(string name) => name.Split(' ').ToStrings(out _firstName, out _lastName);
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
        public override string ToString() => $"First Name: {FirstName}, Last Name: {LastName}";
        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }

    public static class StringArrayExtensions
    {
        public static void ToStrings(this string[] values, out string value1,out string value2)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length != 2) throw new IndexOutOfRangeException("Only arrays with 2 values allowed");
            value1 = values[0];
            value2 = values[1];
        }
    }

    public static class RacerExtensions
    {
        public static void Deconstruct(this Racer r, out string firstName,out string lastName, out int starts, out int wins)
        {
            firstName = r.FirstName;
            lastName = r.LastName;
            starts = r.Starts;
            wins = r.Wins;
        }

        public static void DeconstructWithExtensionsMethods()
        {
            var racer = Formula1.GetChampions().Where(r => r.LastName == "Brabham").First();
            (string first, string last, _, _) = racer;
            Console.WriteLine($"{first} {last}");
        }
    }

}


