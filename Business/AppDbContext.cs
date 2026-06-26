using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Models;

namespace Business
{
    public class AppDbContext : DbContext
    {
        // DbSets for all Entities
        public DbSet<Person> People { get; set; }
        public DbSet<Creditor> Creditors { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // try read password from environment variable
            var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

            if (!string.IsNullOrEmpty(password))
            {
                var connectionString = $"Server=localhost;Port=3306;Database=FinancialSystemDb;User=root;Password={password};";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                return;
            }

            // try to read appsettings.json (if exists, for others environments)
            try
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                    return;
                }
            }
            catch { /* Ignore file read error */}

            // throw error without exposing the password
            throw new Exception("Database password not configured. Please set the MYSQL_PASSWORD environment variable");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Person Settings
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });

            //Creditor Settings
            modelBuilder.Entity<Creditor>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Document).IsRequired().HasMaxLength(18);
                entity.Property(c => c.Email).HasMaxLength(100);
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Address).HasMaxLength(300);
            });

            //Invoice Settings
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).ValueGeneratedOnAdd();
                entity.Property(i => i.Number).IsRequired().HasMaxLength(20);
                entity.Property(i => i.Value).HasColumnType("decimal(18,2)");
                entity.Property(i => i.Description).HasMaxLength(500);

                //Relationship: Creditor -> Invoice (1:N)
                entity.HasOne(i => i.Creditor)
                      .WithMany()
                      .HasForeignKey(i => i.CreditorId)
                      .OnDelete(DeleteBehavior.Restrict);

                //Relationship: Invoice -> PurchaseOrder (1:1)
                entity.HasOne(i => i.PurchaseOrder)
                      .WithOne(po => po.Invoice)
                      .HasForeignKey<PurchaseOrder>(po => po.InvoiceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //PurchaseOrder Settings
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(po => po.Id);
                entity.Property(po => po.Id).ValueGeneratedOnAdd();
                entity.Property(po => po.Number).IsRequired().HasMaxLength(20);
                entity.Property(po => po.Value).HasColumnType("decimal(18,2)");
                entity.Property(po => po.Description).HasMaxLength(500);
            });
        }
    }
}
