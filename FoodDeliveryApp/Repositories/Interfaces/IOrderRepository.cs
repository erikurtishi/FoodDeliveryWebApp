using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Repositories.Interfaces;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order order);
}