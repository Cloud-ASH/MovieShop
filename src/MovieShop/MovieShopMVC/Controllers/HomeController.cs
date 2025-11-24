using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private IMovieService _movieService;
    private readonly ILogger<HomeController> _logger;

    // Genre ID to Name mapping
    private static readonly Dictionary<int, string> GenreNames = new Dictionary<int, string>
    {
        { 1, "Adventure" },
        { 2, "Fantasy" },
        { 3, "Animation" },
        { 4, "Drama" },
        { 5, "Horror" },
        { 6, "Action" },
        { 7, "Comedy" },
        { 8, "History" },
        { 9, "Western" },
        { 10, "Thriller" },
        { 11, "Crime" },
        { 12, "Documentary" },
        { 13, "Science Fiction" },
        { 14, "Mystery" },
        { 15, "Music" },
        { 16, "Romance" },
        { 17, "Family" },
        { 18, "War" },
        { 19, "Foreign" },
        { 20, "TV Movie" }
    };

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    public async Task<IActionResult> Index(int? genreId)
    {
        IEnumerable<ApplicationCore.Models.MovieCardModel> movies;
        
        if (genreId.HasValue)
        {
            movies = await _movieService.GetMoviesByGenre(genreId.Value);
            ViewData["GenreId"] = genreId.Value;
            ViewData["GenreName"] = GenreNames.ContainsKey(genreId.Value) ? GenreNames[genreId.Value] : "Movies";
        }
        else
        {
            movies = await _movieService.GetHighestGrossingMovies();
            ViewData["GenreName"] = "All Movies";
        }
        
        return View(movies);
    }

    public IActionResult Privacy()
    {
        ViewData["Message"] = "Privacy Policy";
        ViewData["Add"] = 30;
        return View();
    }

    public IActionResult TopMovies()
    {
        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}