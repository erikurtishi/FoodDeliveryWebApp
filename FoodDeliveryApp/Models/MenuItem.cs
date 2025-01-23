using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Models;

public class MenuItem
{
    [Key]
    public int MenuItemID { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string PhotoUrl { get; set; } 

    public bool IsAvailable { get; set; } = true;
    
    [Required]
    public int RestaurantID { get; set; }

    [ForeignKey("RestaurantID")]
    public Restaurant Restaurant { get; set; }

    [Required]
    public int CategoryID { get; set; } 

    [ForeignKey("CategoryID")]
    public Category Category { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}