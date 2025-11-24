using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}