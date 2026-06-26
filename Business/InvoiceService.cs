using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public void AddInvoice(string number, DateTime issueDate, decimal value, string descripton, int creditorId)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Invoice number is required!");

            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero!");

            if (creditorId <= 0)
                throw new ArgumentException("A valid Creditor is required!");

            // Check if the Creditor exists
            var creditorExists = _context.Creditors.Any(c => c.Id == creditorId);
            if (!creditorExists)
                throw new ArgumentException("Creditor not found!");

            var invoice = new Invoice
            {
                Number = number,
                IssueDate = issueDate,
                Value = value,
                Description = descripton,
                CreditorId = creditorId
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public List<Invoice> GetAllInvoices()
        {
            return _context.Invoices
                .Include(i => i.Creditor) // Load Creditors data together
                .ToList();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _context.Invoices
                .Include(i => i.Creditor)
                .FirstOrDefault(i => i.Id == id);
        }

        public int CountInvoices()
        {
            return _context.Invoices.Count();
        }

        public List<Creditor> GetAllCreditors()
        {
            return _context.Creditors.ToList();
        }
    }
}
