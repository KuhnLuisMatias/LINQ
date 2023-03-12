using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Racer
{
    public class Racer : IComparable<Racer>, IFormattable
    {
        public Racer(string firstName, string lastName, string country, int starts, int wins) : this(firstName, lastName, country, starts, wins, null, null) { }
        public Racer(string firstName, string lastName, string country, int starts, int wins, IEnumerable<int> years, IEnumerable<string> cars)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Starts = starts;
            Wins = wins;
            Years = years != null ? new List<int>(years) : new List<int>();
            Cars = cars != null ? new List<string>(cars) : new List<string>();
        }

        private static List<Team> s_teams;
        public static IList<Team> GetContructorChampions() => s_teams ?? (s_teams = InitializeRacers);
        private static List<Team> InitializeRacers => new List<Team>
            {
                new Team("Vanwall", 1958),
                new Team("Cooper", 1959, 1960),
                new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999,2000, 2001, 2002, 2003, 2004, 2007, 2008),
                new Team("BRM", 1962),
                new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                new Team("Brabham", 1966, 1967),
                new Team("Matra", 1969),
                new Team("Tyrrell", 1971),
                new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996,1997),
                new Team("Benetton", 1995),
                new Team("Renault", 2005, 2006),
                new Team("Brawn GP", 2009),
                new Team("Red Bull Racing", 2010, 2011, 2012, 1013),
                new Team("Mercedes", 2014, 2015, 2016, 2017)
        };
    
        public string FirstName { get; }
        public string LastName { get; }
        public int Wins { get; }
        public string Country { get; }
        public int Starts { get; }
        public IEnumerable<string> Cars { get; }
        public IEnumerable<int> Years { get; }
        public override string ToString() => $"{FirstName} {LastName}";
        public int CompareTo(Racer other) => LastName.CompareTo(other?.LastName);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return $"{FirstName} {LastName}, {Country}; starts: {Starts}, wins: {Wins}";
                default:
                    throw new FormatException($"Format {format} not supported");
            }
        }
    }
}
