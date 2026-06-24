using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Business
{
    public class CreditorService
    {
        private readonly AppDbContext _context;

        public CreditorService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public void AddCreditor(string name, string document, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name!");

            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentException("Invalid Document!");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Invalid Email");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Invalid Phone");

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Address is empty!");

            var creditor = new Creditor
            {
                Name = name,
                Document = document,
                Email = email,
                Phone = phone,
                Address = address
            };

            _context.Creditors.Add(creditor);

            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Database error: {innerMessage}");
            }
        }

        public List<Creditor> GetCreditors()
        {
            return _context.Creditors.ToList();
        }

        public int CountCreditors()
        {
            return _context.Creditors.Count();
        }

    }
}
