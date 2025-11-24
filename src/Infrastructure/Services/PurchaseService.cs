using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMovieRepository _movieRepository;

    public PurchaseService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository)
    {
        _purchaseRepository = purchaseRepository;
        _movieRepository = movieRepository;
    }

    public IEnumerable<Purchase> GetUserPurchases(int userId)
    {
        return _purchaseRepository.GetUserPurchases(userId);
    }

    public IEnumerable<UserPurchaseViewModel> GetUserPurchaseDetails(int userId)
    {
        var purchases = _purchaseRepository.GetUserPurchases(userId);
        
        return purchases.Select(p => new UserPurchaseViewModel
        {
            PurchaseId = p.Id,
            MovieId = p.MovieId,
            MovieTitle = p.Movie?.Title,
            PosterUrl = p.Movie?.PosterUrl,
            PurchaseDate = p.PurchaseDateTime,
            Price = p.TotalPrice,
            PurchaseNumber = p.PurchaseNumber.ToString()
        }).ToList();
    }

    public bool HasUserPurchasedMovie(int userId, int movieId)
    {
        return _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
    }

    public bool PurchaseMovie(int userId, int movieId)
    {
        var alreadyPurchased = _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
        if (alreadyPurchased) return false;

        var movie = _movieRepository.GetByIdAsync(movieId).Result;
        if (movie == null) return false;

        var purchase = new Purchase
        {
            UserId = userId,
            MovieId = movieId,
            PurchaseDateTime = DateTime.Now,
            PurchaseNumber = Guid.NewGuid(),
            TotalPrice = movie.Price ?? 0
        };

        _purchaseRepository.Insert(purchase);
        return true;
    }
}
