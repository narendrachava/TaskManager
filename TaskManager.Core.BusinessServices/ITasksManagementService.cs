using System.Collections.Generic;
using TaskManager.Core.Domain.BusinessModels.Requests;
using TaskManager.Core.Domain.BusinessModels.Responses;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.BusinessServices
{
    public interface ITasksManagementService
    {
        AllTasksResult AddUserTask(TaskRequest request);

        AllTasksResult GetAllUserTasks(AllTasksRequest request);

        AllTasksResult DeleteUserTask(TaskWithIdRequest request);

        TaskResult FindUserTask(TaskWithIdRequest request);

        AllTasksResult UpdateTask(TaskRequest request);

    }
}
