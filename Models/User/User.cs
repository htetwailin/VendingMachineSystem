using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VendingMachineSystem.Models
{
    public class User
    {
        public int id { get; set; }
        [Display(Name = "User Name")]
        public string username { get; set; }
        [Display(Name = "Role")]
        public int roleid { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Phone No")]
        public string? phoneno { get;set; }
        [Display(Name = "Address")]
        public string? address { get; set; }

    }
}
