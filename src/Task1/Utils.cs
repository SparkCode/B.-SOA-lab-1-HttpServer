using System;
using System.Linq;

namespace Serialization
{
    static class Utils
    {
        public static decimal Sum(decimal[] array) => array.Sum();

        public static int Multiplication(int[] array) => array.Aggregate(1, (x, y) => x * y);

        public static decimal[] Concat(decimal[] first, int[] second)
        {
            var secondDecimal = Array.ConvertAll(second, x => (decimal) x);

            return first.Concat(secondDecimal).ToArray();
        }
    }
}
