using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetHighestGrossingMovies();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
    Movie CreateMovie(Movie movie);
}