using FoodDeliveryApp.Models;
using FoodDeliveryApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    // GET: Admin Index
    public IActionResult Index()
    {
        return View();
    }

    // GET: Create User
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    // POST: Create User
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(RegisterView model, string role)
    {
        if (ModelState.IsValid)
        {
            var isCreated = await _adminRepository.CreateUserAsync(model, role);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error occurred while creating the user.");
        }

        return View(model);
    }
}