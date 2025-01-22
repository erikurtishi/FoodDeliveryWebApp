using FoodDeliveryApp.Models;
using FoodDeliveryApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Repositories.Implementations;

public class AdminRepository: IAdminRepository
{
    private readonly UserManager<AppUser> _userManager;

    public AdminRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(RegisterView model, string role)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var user = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber,
            Age = model.Age ?? 0
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return true;
        }

        return false;
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
}