using System;

namespace Models
{
    public class Creditor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        public Creditor()
        {
            RegistrationDate = DateTime.Now;
            Active = true;
        }

        public override string ToString()
        {
            return $"{Name} - {Document}";
        }
    }
}
