using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public bool HasUserPurchasedMovie(int userId, int movieId)
    {
        return _dbContext.Purchases
            .Any(p => p.UserId == userId && p.MovieId == movieId);
    }

    public IEnumerable<Purchase> GetUserPurchases(int userId)
    {
        return _dbContext.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Movie)
            .ToList();
    }

    public void Insert(Purchase purchase)
    {
        _dbContext.Purchases.Add(purchase);
        _dbContext.SaveChanges();
    }

    public IEnumerable<TopMovieViewModel> GetTopMoviesByPurchase(DateTime? fromDate, DateTime? toDate)
    {
        var query = _dbContext.Purchases.AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(p => p.PurchaseDateTime >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(p => p.PurchaseDateTime <= toDate.Value);
        }

        var topMovies = query
            .GroupBy(p => new { p.MovieId, p.Movie!.Title })
            .Select(g => new TopMovieViewModel
            {
                MovieId = g.Key.MovieId,
                Title = g.Key.Title,
                TotalPurchases = g.Count()
            })
            .OrderByDescending(x => x.TotalPurchases)
            .Take(30)
            .ToList();

        // Add ranking
        int rank = 1;
        foreach (var movie in topMovies)
        {
            movie.Rank = rank++;
        }

        return topMovies;
    }
}