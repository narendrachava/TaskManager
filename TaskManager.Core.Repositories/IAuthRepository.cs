using System.Threading.Tasks;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Repositories
{
    public interface IAuthRepository
    {
        Task<CreateUserResult> RegisterUser(string userName, string password);

        Task<User> FindUser(string userName, string password);
    }
}