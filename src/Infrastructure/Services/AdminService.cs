using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMovieRepository _movieRepository;

    public AdminService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository)
    {
        _purchaseRepository = purchaseRepository;
        _movieRepository = movieRepository;
    }

    public IEnumerable<TopMovieViewModel> GetTopMovies(DateTime? fromDate, DateTime? toDate)
    {
        return _purchaseRepository.GetTopMoviesByPurchase(fromDate, toDate);
    }

    public async Task<Movie> CreateMovieAsync(CreateMovieViewModel model, string createdBy)
    {
        var movie = new Movie
        {
            Title = model.Title,
            Overview = model.Overview,
            Tagline = model.TagLine,
            Budget = model.Budget,
            Revenue = model.Revenue,
            ImdbUrl = model.ImdbURL,
            TmdbUrl = model.TmdbUrl,
            PosterUrl = model.PosterUrl,
            BackdropUrl = model.BackdropUrl,
            OriginalLanguage = model.OriginalLanguage,
            ReleaseDate = model.ReleaseDate,
            RunTime = model.RunTime,
            Price = model.Price,
            CreatedBy = createdBy,
            CreatedDate = DateTime.Now,
            MovieGenres = new List<MovieGenre>()
        };

        // Add selected genres to MovieGenres
        if (model.SelectedGenreIds != null && model.SelectedGenreIds.Any())
        {
            foreach (var genreId in model.SelectedGenreIds)
            {
                movie.MovieGenres.Add(new MovieGenre
                {
                    GenreId = genreId,
                    Movie = movie
                });
            }
        }

        return _movieRepository.CreateMovie(movie);
    }
}