using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IFavoriteRepository
{
    bool AddFavorite(int userId, int movieId);
    bool RemoveFavorite(int userId, int movieId);
    bool IsFavorite(int userId, int movieId);
    IEnumerable<Favorite> GetUserFavorites(int userId);
}
