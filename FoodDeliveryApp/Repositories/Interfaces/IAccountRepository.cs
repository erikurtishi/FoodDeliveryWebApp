using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryApp.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<IdentityResult> RegisterAsync(AppUser user, string password);
    Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<bool> IsInRoleAsync(AppUser user, string role);
    Task LogoutAsync();
}