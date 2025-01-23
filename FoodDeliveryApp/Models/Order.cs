using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models;

public class Order
{
    [Key]
    public int OrderID { get; set; }

    // User who placed the order
    [Required]
    public string UserID { get; set; }
    [ForeignKey("UserID")]
    public AppUser User { get; set; }

    // Delivery Person assigned to the order
    public string? DeliveryID { get; set; } // Make nullable
    [ForeignKey("DeliveryID")]
    public AppUser DeliveryPerson { get; set; }


    // Associated restaurant
    [Required]
    public int RestaurantID { get; set; }
    [ForeignKey("RestaurantID")]
    public Restaurant Restaurant { get; set; }

    // Order Details
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation to OrderItems
    public ICollection<OrderItem> OrderItems { get; set; }
}