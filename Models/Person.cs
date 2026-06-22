using System;

namespace Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime DataCadastro { get; set; }

        public Person()
        {
            DataCadastro = DateTime.Now;
        }

        public bool IsOverEighteen()
        {
            return Age >= 18;
        }

        public override string ToString()
        {
            return $"{Name} - {Age} years";
        }
    }
}
