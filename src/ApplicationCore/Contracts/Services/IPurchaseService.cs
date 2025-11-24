using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IPurchaseService
{
    bool PurchaseMovie(int userId, int movieId);
    bool HasUserPurchasedMovie(int userId, int movieId);
    IEnumerable<Purchase> GetUserPurchases(int userId);
    IEnumerable<UserPurchaseViewModel> GetUserPurchaseDetails(int userId);
}
