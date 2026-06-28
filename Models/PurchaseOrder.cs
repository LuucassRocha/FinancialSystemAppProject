using System;

namespace Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string Number { get; set; } //Ex: OC-2026-001
        public DateTime AuthorizationDate { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Foreign Key
        public int InvoiceId { get; set; }

        // Navegation property
        public virtual Invoice Invoice { get; set; }

        public PurchaseOrder()
        {
            RegistrationDate = DateTime.Now;
            AuthorizationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"PURCHASE ORDER {Number} - R$ {Value:F2}";
        }
    }
}
