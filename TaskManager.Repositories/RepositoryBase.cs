

namespace TaskManager.Repositories
{
    public class RepositoryBase
    {
        protected readonly TaskManagerContext _ctx;
        protected RepositoryBase()
        {
            _ctx = new TaskManagerContext();
        }
    }
}
