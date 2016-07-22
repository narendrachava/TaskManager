

namespace TaskManager.Core.Domain.BusinessModels
{
    public class UserTaskModel
    {
        public int UserTaskId { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }
    }
}