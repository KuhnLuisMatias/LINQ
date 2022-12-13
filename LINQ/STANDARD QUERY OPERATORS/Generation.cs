using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.STANDARD_QUERY_OPERATORS
{
    internal static class Generation
    {
        public static void Range()
        {
            IEnumerable<int> enteros = Enumerable.Range(1, 10).Select(i => i);

            foreach (var val in enteros) Console.WriteLine(val);
        }
        public static void Repeat()
        {
            IEnumerable<string> mensajes = Enumerable.Repeat("Esto es una prueba", 10);
            foreach (var val in mensajes) Console.WriteLine(val);
        }
        public static void RangeRepeat()
        {
            var maxValue = 10;
            IEnumerable<int>enteros = Enumerable.Range(1,maxValue).Select(i => i);
            IEnumerable<string> mensajes = Enumerable.Repeat("Mensaje con valor: ", maxValue);
            List<bool> booleanos = new List<bool>();

            var random = new Random();
            var randomBool = false;

            for(int i=0;i<maxValue; i++)
            {
                randomBool = random.Next(2) == 1;
                booleanos.Add(randomBool);
            }

            var combinarRangeRepeat = enteros.Zip(mensajes, (primerElemento, segundoElemento) => primerElemento + " " + segundoElemento);
            var combinarRangeRepeatBooleanos = combinarRangeRepeat.Zip(booleanos, (primerElemento, segundoElemento) => primerElemento + segundoElemento);

            foreach (var msg in combinarRangeRepeatBooleanos) Console.WriteLine(msg);
        }
    }
}
