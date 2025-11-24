using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IPurchaseService _purchaseService;

    public UserController(IUserService userService, IPurchaseService purchaseService)
    {
        _userService = userService;
        _purchaseService = purchaseService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Purchases()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login");
        }

        int userId = int.Parse(userIdString);
        var purchases = _purchaseService.GetUserPurchaseDetails(userId);

        return View(purchases);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userService.ValidateUser(model.Email, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        var roleId = await _userService.GetUserRoleIdAsync(user.Id);
        
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("RoleId", roleId?.ToString() ?? "2");
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingUser = await _userService.GetUserByEmail(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(model);
        }

        var result = await _userService.RegisterUser(model);
        if (result)
        {
            var user = await _userService.GetUserByEmail(model.Email);
            var roleId = await _userService.GetUserRoleIdAsync(user.Id);
            
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("RoleId", roleId?.ToString() ?? "2");
            
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Registration failed");
        return View(model);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}