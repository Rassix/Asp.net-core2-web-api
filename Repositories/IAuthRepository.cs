using DatingApp.API.Models;
using System.Threading.Tasks;

namespace DatingApp.API.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Authenticate(string username, string password);
        Task<bool> UserExists(string username);
    }
}
