using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Business
{
    public class PersonService
    {
        private readonly AppDbContext _context;

        public PersonService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated(); // Create DB if not exists
        }

        public void AddPerson(string name, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name!");

            if (age < 0 || age > 150)
                throw new ArgumentException("Invalid age!");

            var person = new Person
            {
                Name = name,
                Age = age
            };

            _context.People.Add(person);
            _context.SaveChanges();
        }

        public List<Person> GetPeople()
        {
            return _context.People.ToList();
        }

        public List<Person> GetOverEighteen()
        {
            return _context.People.Where(p => p.IsOverEighteen()).ToList();
        }

        public int CountPeople()
        {
            return _context.People.Count();
        }
    }
}
