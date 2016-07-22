using System.Threading.Tasks;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.BusinessServices
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Create a user in the system and returns the auth token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<CreateUserResult> CreateUser(string userName, string password);

        Task<User> FindUser(string userName, string password);
    }
}
