using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository : IRepository<Purchase>
{
    bool HasUserPurchasedMovie(int userId, int movieId);
    IEnumerable<Purchase> GetUserPurchases(int userId);
    void Insert(Purchase purchase);
    IEnumerable<TopMovieViewModel> GetTopMoviesByPurchase(DateTime? fromDate, DateTime? toDate);
}