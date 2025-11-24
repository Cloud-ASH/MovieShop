using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    private readonly IPurchaseService _purchaseService;
    private readonly IFavoriteService _favoriteService;

    public AccountController(IPurchaseService purchaseService, IFavoriteService favoriteService)
    {
        _purchaseService = purchaseService;
        _favoriteService = favoriteService;
    }

    public IActionResult Index()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "User");
        }

        return View();
    }

    [HttpPost]
    public IActionResult PurchaseMovie(int id)
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "User");
        }

        var userId = int.Parse(userIdString);
        var result = _purchaseService.PurchaseMovie(userId, id);

        if (result)
        {
            TempData["SuccessMessage"] = "Movie purchased successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "You already own this movie or purchase failed.";
        }

        return RedirectToAction("Details", "Movies", new { id });
    }

    [HttpPost]
    public IActionResult ToggleFavorite(int id)
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "User");
        }

        var userId = int.Parse(userIdString);
        _favoriteService.ToggleFavorite(userId, id);

        return RedirectToAction("Details", "Movies", new { id });
    }

    public IActionResult Movies()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "User");
        }

        var userId = int.Parse(userIdString);
        var purchases = _purchaseService.GetUserPurchases(userId);
        var movieCards = purchases.Select(p => new MovieCardModel
        {
            Id = p.Movie.Id,
            Title = p.Movie.Title,
            PosterUrl = p.Movie.PosterUrl
        }).ToList();

        return View(movieCards);
    }

    public IActionResult Favorites()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "User");
        }

        var userId = int.Parse(userIdString);
        var favorites = _favoriteService.GetUserFavorites(userId);
        var movieCards = favorites.Select(f => new MovieCardModel
        {
            Id = f.Movie.Id,
            Title = f.Movie.Title,
            PosterUrl = f.Movie.PosterUrl
        }).ToList();

        return View(movieCards);
    }
}