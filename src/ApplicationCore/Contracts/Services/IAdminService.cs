using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{
    IEnumerable<TopMovieViewModel> GetTopMovies(DateTime? fromDate, DateTime? toDate);
    Task<Movie> CreateMovieAsync(CreateMovieViewModel model, string createdBy);
}