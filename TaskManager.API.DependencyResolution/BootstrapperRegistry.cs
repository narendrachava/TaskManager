using StructureMap;
using TaskManager.Core.BusinessServices;
using TaskManager.Core.Repositories;
using TaskManager.Repositories;
using TaskManger.BusinessServices;

namespace TaskManager.API.DependencyResolution
{
    public class BootstrapperRegistry: Registry
    {
        public BootstrapperRegistry()
        {
            For<IAuthenticationService>().Use<AuthenticationService>();
            For<IAuthRepository>().Use<AuthRepository>();
            For<ITasksManagementService>().Use<TasksManagementService>();
            For<ITasksRepository>().Use<TasksRepository>();
        }
    }
}
