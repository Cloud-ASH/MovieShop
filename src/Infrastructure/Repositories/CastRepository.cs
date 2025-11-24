using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : Repository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Cast?> GetByIdAsync(int id)
    {
        return await GetCastByIdAsync(id);
    }

    public async Task<Cast?> GetCastByIdAsync(int id)
    {
        return await _dbContext.Casts
            .Include(c => c.MovieCasts!)
                .ThenInclude(mc => mc.Movie)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}