using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.UserViewModels;
public class PasswordViewModel
{
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", 
        ErrorMessage = "Please Enter Valid Password, Refer Password Guide")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password and Confirm Password should be same")]
    public string ConfirmPassword { get; set; }
}
