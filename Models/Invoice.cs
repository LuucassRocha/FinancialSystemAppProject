using System;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Number { get; set; }  //Ex: NF-2026-001
        public DateTime IssueDate { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Foreign Keys (relationship)
        public int CreditorId { get; set; }
        public int? PurchaseOrderId { get; set; } //Nullable until linked

        // Navegation properties (For EF Core)
        public virtual Creditor Creditor { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public Invoice()
        {
            RegistrationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"INVOICE {Number} - R$ {Value:F2} - {IssueDate:dd/MM/yyyy}";
        }
    }
}
