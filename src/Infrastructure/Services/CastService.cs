using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<CastDetailsModel?> GetCastDetails(int id)
    {
        var cast = await _castRepository.GetCastByIdAsync(id);
        if (cast == null)
        {
            return null;
        }

        var castDetails = new CastDetailsModel
        {
            Id = cast.Id,
            Name = cast.Name,
            Gender = cast.Gender,
            ProfilePath = cast.ProfilePath,
            TmdbUrl = cast.TmdbUrl,
            Movies = cast.MovieCasts?.Select(mc => mc.Movie).ToList() ?? new()
        };

        return castDetails;
    }
}