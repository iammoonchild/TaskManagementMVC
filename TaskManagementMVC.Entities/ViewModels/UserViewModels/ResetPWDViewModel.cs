using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.UserViewModels;
public class ResetPWDViewModel
{
    [Required]
    public string Email { get; set; }
    public PasswordViewModel Passwords { get; set; }
}
