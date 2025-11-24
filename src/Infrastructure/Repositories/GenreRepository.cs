using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _dbContext.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();
    }
}
