using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface ICastRepository : IRepository<Cast>
{
    Task<Cast?> GetCastByIdAsync(int id);
}