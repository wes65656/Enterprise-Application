using System.ComponentModel.DataAnnotations;

namespace NSE.WebMvc.APP.Models;

public class UserRegister
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
    
    [Display(Name = "Password Confirm")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")] 
    public string ConfirmPassword { get; set; }
}

public class UserLogin
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}