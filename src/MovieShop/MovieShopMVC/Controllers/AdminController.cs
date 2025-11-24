using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IGenreService _genreService;

    public AdminController(IAdminService adminService, IGenreService genreService)
    {
        _adminService = adminService;
        _genreService = genreService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult TopMovies(DateTime? fromDate, DateTime? toDate)
    {
        // Check if user is admin (RoleId == "1")
        var roleId = HttpContext.Session.GetString("RoleId");
        if (roleId != "1")
        {
            return RedirectToAction("Index", "Home");
        }

        var topMovies = _adminService.GetTopMovies(fromDate, toDate);

        ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
        ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

        return View(topMovies);
    }

    [HttpGet]
    public async Task<IActionResult> CreateMovie()
    {
        var roleId = HttpContext.Session.GetString("RoleId");
        if (roleId != "1")
        {
            return RedirectToAction("Index", "Home");
        }

        // Load all genres for dropdown
        var genres = await _genreService.GetAllGenresAsync();
        ViewBag.Genres = genres;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(CreateMovieViewModel model)
    {
        var roleId = HttpContext.Session.GetString("RoleId");
        if (roleId != "1")
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            // Reload genres if validation fails
            var genres = await _genreService.GetAllGenresAsync();
            ViewBag.Genres = genres;
            return View(model);
        }

        try
        {
            var userName = HttpContext.Session.GetString("UserName") ?? "Admin";
            var movie = await _adminService.CreateMovieAsync(model, userName);

            TempData["SuccessMessage"] = $"Movie '{movie.Title}' created successfully!";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating movie: {ex.Message}");
            
            // Reload genres
            var genres = await _genreService.GetAllGenresAsync();
            ViewBag.Genres = genres;
            return View(model);
        }
    }
}