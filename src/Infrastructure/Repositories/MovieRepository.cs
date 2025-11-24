using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
    {
        return await _dbContext.Movies
            .OrderByDescending(m => m.Revenue)
            .Take(30)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
    {
        return await _dbContext.Movies
            .Where(m => m.MovieGenres!.Any(mg => mg.GenreId == genreId))
            .OrderByDescending(m => m.Revenue)
            .Take(30)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _dbContext.Movies
            .Include(m => m.MovieGenres!)
                .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCasts!)
                .ThenInclude(mc => mc.Cast)
            .Include(m => m.Trailers)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public Movie CreateMovie(Movie movie)
    {
        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();
        return movie;
    }
}