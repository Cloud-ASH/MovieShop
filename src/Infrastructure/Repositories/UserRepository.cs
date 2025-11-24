using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AssignRoleAsync(UserRole userRole)
    {
        _dbContext.UserRoles.Add(userRole);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int?> GetUserRoleIdAsync(int userId)
    {
        var userRole = await _dbContext.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId);
        return userRole?.RoleId;
    }
}