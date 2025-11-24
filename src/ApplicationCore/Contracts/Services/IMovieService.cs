using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    //all the business logic methods relating to movies entity
    List<MovieCardModel> GetTop30GrossingMovies();
    Task<IEnumerable<MovieCardModel>> GetHighestGrossingMovies();
    Task<MovieDetailsModel?> GetMovieDetails(int id, int? userId = null);
    Task<IEnumerable<MovieCardModel>> GetMoviesByGenre(int genreId);
}