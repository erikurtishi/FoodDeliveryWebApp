using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Repositories.Interfaces;

public interface IAdminRepository
{
    Task<bool> CreateUserAsync(RegisterView model, string role);
}