using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Repositories.Interfaces;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetRestaurantWithMenuAsync(int id);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(int id);
}