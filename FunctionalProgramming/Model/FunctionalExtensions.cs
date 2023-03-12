using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FunctionalProgramming.Model
{
    public static class FunctionalExtensions
    {
        public static Func<T1, TResult> Compose<T1, T2, TResult>(Func<T1, T2> f1, Func<T2, TResult> f2) => a => f2(f1(a));
    }


}
