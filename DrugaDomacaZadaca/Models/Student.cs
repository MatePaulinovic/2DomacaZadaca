using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }
        public bool Equals(Student another)
        {
            if (Jmbag.Equals(another.Jmbag))
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            if(Name == null || Jmbag == null)
            {
                return 0;
            }
            else
            {
                return Name.GetHashCode() ^ Jmbag.GetHashCode();
            }
        }

    }
    public enum Gender
    {
        Male, Female
    }
}

