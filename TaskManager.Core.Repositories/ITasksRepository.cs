using System.Collections.Generic;
using TaskManager.Core.Domain.BusinessModels.Requests;
using TaskManager.Core.Domain.BusinessModels.Responses;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Repositories
{
    public interface ITasksRepository
    {
        AllTasksResult CreateTask(TaskRequest task);

        AllTasksResult UpdateTask(TaskRequest task);

        AllTasksResult DeleteTask(TaskWithIdRequest taskId);

        TaskResult GetTask(TaskWithIdRequest request);

        AllTasksResult GetAllTasks(AllTasksRequest request);
    }
}
