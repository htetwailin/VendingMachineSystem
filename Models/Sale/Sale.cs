using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachineSystem.Models.Sale
{
    public class Sale
    {
        public int id { get; set; }
       
        [Display(Name = "User Name")]
        public int userid { get; set; }
        [ForeignKey("userid")]
        public virtual User User { get; set; }
        [Display(Name = "Voucher No")]
        public string voucherno { get; set; }
        [Display(Name = "Total")]
        public decimal total { get; set; }
        [Display(Name = "Phone No")]
        public string phoneno { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Sale Date")]
        public DateTime saledate { get; set; }
        public List<saleDetail> saleDetail { get; set; }
    }
    public class saleDetail
    {
        public int id { get; set; }
        public int saleid { get; set; }
        public int product_id { get; set; }
        public int qty { get; set; }  
        public decimal price { get; set; }
    }
}
