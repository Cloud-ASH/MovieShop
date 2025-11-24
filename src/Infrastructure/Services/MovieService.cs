using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IFavoriteRepository _favoriteRepository;

    public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
    {
        _movieRepository = movieRepository;
        _purchaseRepository = purchaseRepository;
        _favoriteRepository = favoriteRepository;
    }

    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        var movies = new List<MovieCardModel>()
        {
            new MovieCardModel() { Id = 1, Title = "Inception", PosterUrl = "https://www.imdb.com/title/tt1375666/mediaviewer/rm1755533569/?ref_=tt_ov_i" },
            new MovieCardModel() { Id = 2, Title = "Interstellar", PosterUrl = "https://www.imdb.com/title/tt0816692/mediaviewer/rm3201653505/?ref_=tt_ov_i" },
            new MovieCardModel() { Id = 3, Title = "The Dark Knight", PosterUrl = "https://www.imdb.com/title/tt0468569/mediaviewer/rm4023877632/?ref_=tt_ov_i" }
        };
        return movies;
    }

    public async Task<IEnumerable<MovieCardModel>> GetHighestGrossingMovies()
    {
        var movies = await _movieRepository.GetHighestGrossingMovies();
        var movieCards = movies.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title ?? "",
            PosterUrl = m.PosterUrl ?? ""
        });
        return movieCards;
    }

    public async Task<MovieDetailsModel?> GetMovieDetails(int id, int? userId = null)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return null;
        }

        var movieDetails = new MovieDetailsModel
        {
            Id = movie.Id,
            IsPurchased = userId.HasValue && _purchaseRepository.HasUserPurchasedMovie(userId.Value, id),
            IsFavorite = userId.HasValue && _favoriteRepository.IsFavorite(userId.Value, id),
            Title = movie.Title,
            PosterUrl = movie.PosterUrl,
            BackdropUrl = movie.BackdropUrl,
            Rating = movie.Rating,
            Overview = movie.Overview,
            Tagline = movie.Tagline,
            Budget = movie.Budget,
            Revenue = movie.Revenue,
            ImdbUrl = movie.ImdbUrl,
            TmdbUrl = movie.TmdbUrl,
            ReleaseDate = movie.ReleaseDate,
            RunTime = movie.RunTime,
            Price = movie.Price,
            OriginalLanguage = movie.OriginalLanguage,
            Genres = movie.MovieGenres?.Select(mg => mg.Genre).ToList() ?? new List<Genre>(),
            Casts = movie.MovieCasts?.Select(mc => mc.Cast).ToList() ?? new List<Cast>(),
            Trailers = movie.Trailers?.ToList() ?? new List<Trailer>(),
            Reviews = movie.Reviews?.ToList() ?? new List<Review>()
        };

        return movieDetails;
    }

    public async Task<IEnumerable<MovieCardModel>> GetMoviesByGenre(int genreId)
    {
        var movies = await _movieRepository.GetMoviesByGenre(genreId);
        var movieCards = movies.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = m.PosterUrl
        });
        return movieCards;
    }
}