using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class PurchaseOrderService
    {
        private readonly AppDbContext _context;

        public PurchaseOrderService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public void AddPurchaseOrder(string number, DateTime authorizationDate, decimal value, string description, int invoiceId)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Purchase Order number is required!");

            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero!");

            if (invoiceId <= 0)
                throw new ArgumentException("A valid Invoice is required!");

            // Check if the Invoice exists
            var invoiceExists = _context.Invoices.Any(i => i.Id == invoiceId);
            if (!invoiceExists)
                throw new ArgumentException("Invoice not found!");

            var purchaseOrder = new PurchaseOrder
            {
                Number = number,
                AuthorizationDate = authorizationDate,
                Value = value,
                Description = description,
                InvoiceId = invoiceId
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChanges();
        }

        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            return _context.PurchaseOrders
                .Include(po => po.Invoice) // Load Invoices data togheter
                .ToList();
        }

        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return _context.PurchaseOrders
                .Include(po => po.Invoice)
                .FirstOrDefault(po => po.Id == id);
        }

        public int CountPurchaseOrders()
        {
            return _context.PurchaseOrders.Count();
        }

        public List<Invoice> GetAllInvoices()
        {
            return _context.Invoices
                .Include(i => i.Creditor)
                .ToList();
        }
    }
}
