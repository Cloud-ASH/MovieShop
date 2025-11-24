using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Services;

public interface IFavoriteService
{
    bool ToggleFavorite(int userId, int movieId);
    bool IsFavorite(int userId, int movieId);
    IEnumerable<Favorite> GetUserFavorites(int userId);
}
