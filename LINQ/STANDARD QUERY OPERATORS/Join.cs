using LINQ.Racer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Join
    {
        public static void InnerJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetContsructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams = (from r in racers
                                  join t in teams on r.Year equals t.Year
                                  select new
                                  {
                                      r.Year,
                                      Champion = r.Name,
                                      Constructor = t.Name
                                  }).Take(10);

            Console.WriteLine("Year World Champion\t Constructor Title");

            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }
        public static void InnerJoin_II()
        {
            var racersAndTeams = (from r in
                                      from r1 in Formula1.GetChampions()
                                      from yr in r1.Years
                                      select new
                                      {
                                          Year = yr,
                                          Name = r1.FirstName + " " + r1.LastName
                                      }
                                  join t in
                                  from t1 in Formula1.GetContsructorChampions()
                                  from yt in t1.Years
                                  select new
                                  {
                                      Year = yt,
                                      Name = t1.Name
                                  }
                                  on r.Year equals t.Year
                                  orderby t.Year
                                  select new
                                  {
                                      Year = r.Year,
                                      Racer = r.Name,
                                      Team = t.Name
                                  }).Take(10);

            Console.WriteLine("Year World Champion\t Constructor Title");

            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Racer,-20} {item.Team}");
            }

        }
        public static void InnerJoinWithMethods()
        {
            var racers = Formula1.GetChampions()
                        .SelectMany(r => r.Years, (r1, year) => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });

            var teams = Formula1.GetContsructorChampions()
                        .SelectMany(t => t.Years, (t, year) => new { Year = year, Name = t.Name });

            var racersAndTeams = racers.Join(teams, r => r.Year, t => t.Year, (r, t) =>
                        new { Year = r.Year, Champion = r.Name, Constructor = t.Name }).OrderBy(item => item.Year)
                        .Take(10);

            Console.WriteLine("Year World Champion\t Constructor Title");

            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }
        public static void LeftOuterJoin()
        {
            var racers = Formula1.GetChampions()
                       .SelectMany(r => r.Years, (r1, year) => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });

            var teams = Formula1.GetContsructorChampions()
                        .SelectMany(t => t.Years, (t, year) => new { Year = year, Name = t.Name });

            var racersAndTeams =
                (from r in racers
                 join t in teams on r.Year equals t.Year into rt
                 from t in rt.DefaultIfEmpty()
                 orderby r.Year
                 select new
                 {
                     Year = r.Year,
                     Champion = r.Name,
                     Constructor = t == null ? "no constructor championship" : t.Name
                 }).Take(10);

            Console.WriteLine("Year World Champion\t Constructor Title");

            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }
        public static void LeftOuterJoinWithMethods()
        {
            var racers = Formula1.GetChampions()
                     .SelectMany(r => r.Years, (r1, year) => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });

            var teams = Formula1.GetContsructorChampions()
                        .SelectMany(t => t.Years, (t, year) => new { Year = year, Name = t.Name });

            var racersAndTeams = racers.GroupJoin(teams, r => r.Year, t => t.Year, (r, ts) => new
            {
                Year = r.Year,
                Champion = r.Name,
                Constructors = ts
            })
                                                    .SelectMany(
                                                    rt => rt.Constructors.DefaultIfEmpty(),
                                                    (r, t) => new
                                                    {
                                                        Year = r.Year,
                                                        Champion = r.Champion,
                                                        Constructor = t?.Name ?? "no constructor championship"
                                                    });

        }

        #region NoFunciona
        /*
        public static void GroupJoin()
        {
            var racers = from cs in Formula1.GetChampionships()
                         from r in new List<(int Year, int Position, string FirstName, string LastName)>()
                         {
                            (cs.Year, Position: 1, FirstName: cs.First.FirstName(),
                            LastName: cs.First.LastName()),
                            (cs.Year, Position: 2, FirstName: cs.Second.FirstName(),
                            LastName: cs.Second.LastName()),
                            (cs.Year, Position: 3, FirstName: cs.Third.FirstName(),
                            LastName: cs.Third.LastName())
                         }
                         select r;
        }

        public static void GroupJoin()
        {
            var racers = Formula1.GetChampions()
                    .SelectMany(r => r.Years, (r1, year) => new { Year = year, Name = $"{r1.FirstName} {r1.LastName}" });

            var q = (from r in Formula1.GetChampions()
                     join r2 in racers on (r.FirstName, r.LastName) equals (r2.FirstName, r2.LastName)
                     into yearResults
                     select r.FirstName, r.LastName, r.Wins, r.Starts, Results: yearResults));
            foreach (var r in q)
            {
                Console.WriteLine($"{r.FirstName} {r.LastName}");
                foreach (var results in r.Results)
                {
                    Console.WriteLine($"\t{results.Year} {results.Position}");
                }
            }
        }

        public static void GroupJoinWithMethods()
        {
            var racers = Formula1.GetChampionships()
            .SelectMany(cs => new List<(int Year, int Position, string FirstName, string LastName)>
            {
                (cs.Year, Position: 1, FirstName: cs.First.FirstName(),
                LastName: cs.First.LastName()),
                (cs.Year, Position: 2, FirstName: cs.Second.FirstName(),
                LastName: cs.Second.LastName()),
                (cs.Year, Position: 3, FirstName: cs.Third.FirstName(),
                LastName: cs.Third.LastName())
            });

            var q = Formula1.GetChampions()
                            .GroupJoin(racers,
                            r1 => (r1.FirstName, r1.LastName),
                            r2 => (r2.FirstName, r2.LastName),
                            (r1, r2s) => (r1.FirstName, r1.LastName, r1.Wins, r1.Starts,
                            Results: r2s));
        }
        */
        #endregion
    }
}
