using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Repositories.Interfaces;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;

[Authorize(Roles = "Restaurant")]
public class RestaurantController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public RestaurantController(IOrderRepository orderRepository, AppDbContext context, UserManager<AppUser> userManager)
    {
        _orderRepository = orderRepository;
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var userEmail = User.Identity?.Name;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in.");
        }

        var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (appUser == null)
        {
            return Unauthorized("Invalid user.");
        }

        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.UserID == appUser.Id);
        return restaurant != null ? View(restaurant) : RedirectToAction("CreateRestaurant");
    }


    // GET: Menu Items
    public async Task<IActionResult> MenuItems()
    {
        var userEmail = User.Identity?.Name;

        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in.");
        }

        var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (appUser == null)
        {
            return Unauthorized("Invalid user.");
        }

        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.UserID == appUser.Id);
        if (restaurant == null)
        {
            return NotFound("Restaurant not found.");
        }

        var menuItems = await _context.MenuItems
            .Where(mi => mi.RestaurantID == restaurant.RestaurantID)
            .Include(mi => mi.Category)
            .ToListAsync();

        ViewData["RestaurantName"] = restaurant.Name;

        return View(menuItems);
    }

    // GET: Create Menu Item
    public async Task<IActionResult> CreateMenuItem()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewData["Categories"] = categories;

        return View();
    }

    // POST: Create Menu Item
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMenuItem(MenuItem model)
    {
        var userEmail = User.Identity?.Name;

        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in.");
        }

        var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (appUser == null)
        {
            return Unauthorized("Invalid user.");
        }

        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.UserID == appUser.Id);
        if (restaurant == null)
        {
            return NotFound("Restaurant not found.");
        }

        if (!await _context.Categories.AnyAsync(c => c.CategoryID == model.CategoryID))
        {
            ModelState.AddModelError("CategoryID", "Invalid category selected.");
            ViewData["Categories"] = await _context.Categories.ToListAsync();
            return View(model);
        }

        model.RestaurantID = restaurant.RestaurantID;
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;

        _context.MenuItems.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("MenuItems");
    }
}
