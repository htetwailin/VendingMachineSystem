using Microsoft.EntityFrameworkCore;

namespace VendingMachineSystem.Data
{
    public class VendingMachineDBContext : DbContext
    {
        public VendingMachineDBContext (DbContextOptions<VendingMachineDBContext> options)
            : base(options)
        {
        }

       
        public DbSet<VendingMachineSystem.Models.User>? User { get; set; }
        public DbSet<VendingMachineSystem.Models.Product.Product>? Product { get; set; }
        public DbSet<VendingMachineSystem.Models.Purchase.Purchase>? Purchase { get; set; }
        public DbSet<VendingMachineSystem.Models.Purchase.PurchaseDetail>? PurchaseDetail { get; set; }
        public DbSet<VendingMachineSystem.Models.Sale.Sale>? Sale { get; set; }
        public DbSet<VendingMachineSystem.Models.Sale.saleDetail>? SaleDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
    }
}
