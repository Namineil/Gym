using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services {
    public interface IAuthService
    {
        Task<User> Authenticate(string login, string password, bool rememberMe);
        Task<User> ValidateToken(string token);
        string hashPwd(string password);
    }
}