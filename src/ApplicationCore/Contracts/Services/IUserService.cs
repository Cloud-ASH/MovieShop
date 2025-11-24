using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<User?> ValidateUser(string email, string password);
    Task<User?> GetUserByEmail(string email);
    Task<bool> RegisterUser(RegisterRequestModel model);
    Task<int?> GetUserRoleIdAsync(int userId);
}