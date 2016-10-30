using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            string[] strings = integers.GroupBy(i => i).Select(j => "Broj " + j.Key + " se ponavlja " + j.Count() + " puta").ToArray();
            for (int i = 0; i < strings.Length; i++)
            {
                Console.WriteLine(strings[i]);
            }
            // strings [0] = Broj 1 ponavlja se 1 puta
            // strings [1] = Broj 2 ponavlja se 3 puta
            // strings [2] = Broj 3 ponavlja se 2 puta
            // strings [3] = Broj 4 ponavlja se 1 puta
            // strings [4] = Broj 5 ponavlja se 1 puta

            Example1();
            Example2();

            University[] universities = new University[]
            {
                new University(),
                new University(),
                new University(),
                new University()
            };

            Student[] allCroatianStudents = universities.SelectMany(i => i.Students).Distinct().ToArray();
            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(i => i.Students).GroupBy(j => j).Where(k => k.Count() > 1).Select(l => l.Key).ToArray();
            Student[] studentsOnMaleOnlyUniversities = universities.Select(i => i).Where(j => j.Students.Count(k => k.Gender == Gender.Female) == 0).SelectMany(l => l.Students).ToArray();
        }
        public static void Example1()
        {
            var list = new List<Student>()
            {
            new Student (" Ivan ", jmbag :" 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            // false :(
            bool anyIvanExists = list.Any(s => s.Equals(ivan));
            if (anyIvanExists)
            {
                Console.WriteLine("Postoji");
            }
        }
        public static void Example2()
        {
            var list = new List<Student>()
            {
            new Student (" Ivan ", jmbag :" 001234567 ") ,
            new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();
            Console.WriteLine(distinctStudents);
        }

    }
}
