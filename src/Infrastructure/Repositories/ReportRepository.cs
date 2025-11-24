using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly MovieShopDbContext _dbContext;

    public ReportRepository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}