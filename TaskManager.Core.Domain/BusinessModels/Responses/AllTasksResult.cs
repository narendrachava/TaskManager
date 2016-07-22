using System.Collections.Generic;


namespace TaskManager.Core.Domain.BusinessModels.Responses
{
    public class AllTasksResult: BaseResult
    {
        public IEnumerable<UserTaskModel> Tasks { get; set; }
    }
}
