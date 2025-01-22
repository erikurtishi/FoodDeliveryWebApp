using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryApp.Models;

public class AppUser : IdentityUser
{
    [Required]
    public string FullName { get; set; }
    public int Age { get; set; }
    [Required]
    public string Address { get; set; }
    public decimal Balance { get; set; } = 0.00m;
}