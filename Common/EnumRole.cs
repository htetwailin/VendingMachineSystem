using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.Common
{
    public enum EnumRole
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2
        //[Display(Name = "SuperAdmin")]
        //SuperAdmin = 3
    }
    public enum EnumGender 
    {
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Female")]
        Female = 2
    }

}
