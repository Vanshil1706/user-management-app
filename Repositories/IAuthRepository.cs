

using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginRequest request);
        Task<bool> Logout();

    }
}
