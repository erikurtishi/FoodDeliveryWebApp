using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;

[Authorize(Roles = "BasicUser")]
public class OrderController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public OrderController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(int menuItemId, int quantity)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account",
                new { returnUrl = Url.Action("Checkout", "Order", new { menuItemId, quantity }) });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var menuItem = await _context.MenuItems.Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.MenuItemID == menuItemId);
        if (menuItem == null)
        {
            return NotFound("Menu item not found.");
        }

        var order = new Order
        {
            UserID = user.Id,
            RestaurantID = menuItem.RestaurantID,
            TotalAmount = menuItem.Price * quantity,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    MenuItemID = menuItem.MenuItemID,
                    Quantity = quantity,
                    TotalPrice = menuItem.Price * quantity
                }
            },
            CreatedAt = DateTime.UtcNow,
            Status = "Pending",
            DeliveryID = null // Explicitly set null (not required if already nullable)
        };


        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        TempData["OrderSuccess"] = "Order placed successfully!";
        return RedirectToAction("Details", "Home", new { id = menuItem.RestaurantID });
    }
}