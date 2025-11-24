using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IReviewRepository _reviewRepository;

        public MoviesController(IMovieService movieService, IReviewRepository reviewRepository)
        {
            _movieService = movieService;
            _reviewRepository = reviewRepository;
        }

        // GET: MoviesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movies/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            int? userId = null;
            if (!string.IsNullOrEmpty(userIdString))
            {
                userId = int.Parse(userIdString);
            }

            var movie = await _movieService.GetMovieDetails(id, userId);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult SubmitReview(int id, decimal rating, string reviewText)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);

            // Check if user already reviewed this movie
            var existingReview = _reviewRepository.GetUserReviewForMovie(userId, id);
            if (existingReview != null)
            {
                TempData["ErrorMessage"] = "You have already reviewed this movie!";
                return RedirectToAction("Details", new { id });
            }

            var review = new Review
            {
                MovieId = id,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText,
                CreatedDate = DateTime.Now
            };

            _reviewRepository.Insert(review);

            TempData["SuccessMessage"] = "Review submitted successfully!";
            return RedirectToAction("Details", new { id });
        }
    }
}
