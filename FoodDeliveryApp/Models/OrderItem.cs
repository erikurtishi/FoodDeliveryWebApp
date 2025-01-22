using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Models;

public class OrderItem
{
    [Key]
    public int OrderItemID { get; set; }

    [Required]
    public int MenuItemID { get; set; } // FK to MenuItem
    [ForeignKey("MenuItemID")]
    public MenuItem MenuItem { get; set; }

    [Required]
    public int OrderID { get; set; } // FK to Order
    [ForeignKey("OrderID")]
    public Order Order { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }
}