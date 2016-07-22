

namespace TaskManager.Core.Domain.BusinessModels.Requests
{
    public class TaskRequest : IAuthenticatedRequest
    {
        public string UserId
        {
            get;set;
        }

        public UserTaskModel Task { get; set; }
    }
}
