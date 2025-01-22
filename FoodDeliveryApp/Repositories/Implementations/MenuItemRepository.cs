using FoodDeliveryApp.Data;
using FoodDeliveryApp.Repositories.Interfaces;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Repositories.Implementations;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly AppDbContext _context;

    public MenuItemRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all menu items
    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems.Include(m => m.Restaurant).Include(m => m.Category).ToListAsync();
    }

    // Get menu items by restaurant ID
    public async Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantAsync(int restaurantId)
    {
        return await _context.MenuItems
            .Where(m => m.RestaurantID == restaurantId)
            .Include(m => m.Category)
            .ToListAsync();
    }

    // Get a menu item by ID
    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems
            .Include(m => m.Restaurant)
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.MenuItemID == id);
    }

    public Task<IEnumerable<MenuItem>> GetAllByRestaurantAsync(int restaurantId)
    {
        throw new NotImplementedException();
    }
    
    public async Task AddAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem != null)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}