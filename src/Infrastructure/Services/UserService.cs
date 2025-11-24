using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> ValidateUser(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null) return null;

        // Hash the provided password with the user's salt
        var hashedPassword = HashPassword(password, user.Salt ?? "");
        
        // Compare with stored hashed password
        if (hashedPassword == user.HashedPassword)
        {
            return user;
        }
        
        return null;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _userRepository.GetUserByEmail(email);
    }

    public async Task<bool> RegisterUser(RegisterRequestModel model)
    {
        // Generate salt
        var salt = GenerateSalt();
        
        // Hash password with salt
        var hashedPassword = HashPassword(model.Password, salt);

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber,
            HashedPassword = hashedPassword,
            Salt = salt
        };

        var newUser = await _userRepository.AddAsync(user);
        
        // Assign default role (RoleId = 2 for regular users)
        var userRole = new UserRole
        {
            UserId = newUser.Id,
            RoleId = 2  // 2 = Regular User, 1 = Admin
        };
        await _userRepository.AssignRoleAsync(userRole);
        
        return true;
    }

    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword(string password, string salt)
    {
        var saltedPassword = password + salt;
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public async Task<int?> GetUserRoleIdAsync(int userId)
    {
        return await _userRepository.GetUserRoleIdAsync(userId);
    }
}