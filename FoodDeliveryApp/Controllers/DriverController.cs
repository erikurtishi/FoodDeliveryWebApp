using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;

[Authorize(Roles = "Driver")]
public class DriverController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public DriverController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Driver Dashboard
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var orders = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .Include(o => o.Restaurant)
            .Where(o => o.DeliveryID == userId && (o.Status == "Assigned to Delivery" || o.Status == "Out for Delivery"))
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return View(orders);
    }

    // POST: Update Order Status
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
    {
        var userId = _userManager.GetUserId(User);

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        if (order.DeliveryID != userId && !User.IsInRole("Restaurant"))
        {
            return Unauthorized("You are not authorized to update this order.");
        }

        order.Status = status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Order status updated successfully!";
        return RedirectToAction("Index");
    }


    // GET: Order Details
    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager.GetUserId(User);

        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .Include(o => o.Restaurant)
            .FirstOrDefaultAsync(o => o.OrderID == id && o.DeliveryID == userId);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return View(order);
    }
}
