using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.BusinessServices;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Repositories;

namespace TaskManger.BusinessServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository _authRepo;

        public AuthenticationService(IAuthRepository authRepository)
        {
            _authRepo = authRepository;
        }
        public async Task<CreateUserResult> CreateUser(string userName, string password)
        {
            var result = await _authRepo.RegisterUser(userName, password);
            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var result = await _authRepo.FindUser(userName, password);
            return result;
        }
    }
}
