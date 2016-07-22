using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Repositories;

namespace TaskManager.Repositories
{
    public class AuthRepository : RepositoryBase, IAuthRepository, IDisposable
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository():base()
        {
            _userManager = new UserManager<User>(new UserStore<User>(_ctx));
        }

        public async Task<CreateUserResult> RegisterUser(string userName, string password)
        {
            var user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            return new CreateUserResult()
            {
                Errors = result.Errors.ToList(),
                IsSucceeded = result.Succeeded
            };
        }

        public async Task<User> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
            return user as User;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
