using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
        private static void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static int GetTheMagicNumber()
        {
            return IKnowIGuyWhoKnowsAGuy();
        }
        private static int IKnowIGuyWhoKnowsAGuy()
        {
            return IKnowWhoKnowsThis(10) + IKnowWhoKnowsThis(5);
        }
        private static int IKnowWhoKnowsThis(int n)
        {
            return FactorialDigitSumAsync(n).Result;
        }

        public static async Task<int> FactorialDigitSumAsync(int x)
        {
            int fact = 1;
            int sum = 0;
            for (int i = 1; i <= x; i++)
            {
                fact *= i;
            }
            while (fact != 0)
            {
                sum += fact % 10;
                fact = fact / 10;
            }
            return sum;
        }
    }
}
