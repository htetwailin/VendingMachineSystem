using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.Models.Purchase
{
    
    public class Purchase
    {
        public int id { get; set; }
        [Display(Name = "Purchase No")]
        public string purchase_no { get; set; }
        [Display(Name = "Total")]
        public decimal total { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime puchanse_date { get; set; }
        
    }
    public class PurchaseDetail
    {
        public int id { get; set; }
        public int purchase_id { get; set; }
        public int product_id { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
    }
    public class PurchaseViewModel
    {
        public int id { get; set; }
        public string purchase_no { get; set; }
        public decimal total { get; set; }
        public DateTime puchanse_date { get; set; }
        public List<PurchaseDetail> items { get; set; }
    }
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }

}
