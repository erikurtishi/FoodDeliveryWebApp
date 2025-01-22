using System.Diagnostics;
using FoodDeliveryApp.Data;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;


    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Display all restaurants on the homepage
        public async Task<IActionResult> Index()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        // Display restaurant details and menu items
        public async Task<IActionResult> Details(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.MenuItems)
                .ThenInclude(m => m.Category) // Include categories for filtering
                .FirstOrDefaultAsync(r => r.RestaurantID == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
    }