using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository
{
    void Insert(Review review);
    bool AddReview(Review review);
    bool UpdateReview(Review review);
    bool DeleteReview(int userId, int movieId);
    Review GetUserReviewForMovie(int userId, int movieId);
    IEnumerable<Review> GetMovieReviews(int movieId);
}
