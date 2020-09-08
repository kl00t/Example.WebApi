using System.Threading.Tasks;
using Example.Client.Models;

namespace Example.Client
{
    public interface IUserClient
    {
        Task<string> CreateUser(User user);

        Task<string> GetUser(long userId);

        Task<string> DeleteUser(long userId);

        Task<string> UpdateUser(long userId, User user);
    }
}