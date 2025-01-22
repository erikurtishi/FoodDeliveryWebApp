using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Repositories.Interfaces;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(int id);
    Task<IEnumerable<MenuItem>> GetAllByRestaurantAsync(int restaurantId);
    Task AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(int id);
}