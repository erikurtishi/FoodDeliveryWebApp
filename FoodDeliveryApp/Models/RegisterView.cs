using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models;

public class RegisterView
{
    [Required(ErrorMessage = "Full Name is required.")]
    [Display(Name = "Full Name")] 
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }
    public int? Age { get; set; }

    [Display(Name = "Phone Number")] 
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirm Password")] 
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}