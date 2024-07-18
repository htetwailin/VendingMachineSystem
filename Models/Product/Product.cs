using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.Models.Product
{
    public class Product
    {
        public int id {  get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Quantity Available")]
        public int qty_abl { get; set; } 
        
    }
}
