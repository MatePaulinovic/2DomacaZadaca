using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad7
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = Int32.Parse(Console.ReadLine());
            CallAsync(x);
        }

        public static async Task CallAsync(int x)
        {
            Console.WriteLine(await FactorialDigitSumAsync(x));
        }

        public static async Task<int> FactorialDigitSumAsync(int x)
        {
            int fact = 1;
            int sum = 0;
            for( int i = 1; i <= x; i++)
            {
                fact *= i;
            }
            while(fact != 0)
            {
                sum += fact % 10;
                fact = fact / 10;
            }
            return sum;
        }
    }
}
