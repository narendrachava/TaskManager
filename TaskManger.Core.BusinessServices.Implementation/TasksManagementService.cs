using System;
using System.Collections.Generic;
using TaskManager.Core.BusinessServices;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.BusinessModels.Requests;
using TaskManager.Core.Domain.BusinessModels.Responses;
using TaskManager.Core.Repositories;

namespace TaskManger.BusinessServices
{
    public class TasksManagementService : ITasksManagementService
    {
        private readonly ITasksRepository _taskRepo;
        public TasksManagementService(ITasksRepository repo)
        {
            _taskRepo = repo;
        }

        public AllTasksResult DeleteUserTask(TaskWithIdRequest request)
        {
            AllTasksResult result = null;
            try
            {
                result = _taskRepo.DeleteTask(request);
            }
            catch (Exception ex)
            {
                if (result == null)
                    result = new AllTasksResult();
                result = PopulateErrorMessage(ex, result) as AllTasksResult;
            }
            return result;
        }

        public TaskResult FindUserTask(TaskWithIdRequest request)
        {
            TaskResult result = null;
            try
            {
                result = _taskRepo.GetTask(request);
            }
            catch (Exception ex)
            {
                if (result == null)
                    result = new TaskResult();
                result = PopulateErrorMessage(ex, result) as TaskResult;
            }
            return result;
        }

        public AllTasksResult AddUserTask(TaskRequest request)
        {
            AllTasksResult result = null;
            try
            {
                result = _taskRepo.CreateTask(request);
            }
            catch (Exception ex)
            {
                if (result == null)
                    result = new AllTasksResult();
                result = PopulateErrorMessage(ex, result) as AllTasksResult;
            }
            return result;
        }

        public AllTasksResult UpdateTask(TaskRequest request)
        {
            AllTasksResult result = null;
            try
            {
                result = _taskRepo.UpdateTask(request);
            }
            catch (Exception ex)
            {
                if (result == null)
                    result = new AllTasksResult();
                result = PopulateErrorMessage(ex, result) as AllTasksResult;
            }
            return result;
        }

        public AllTasksResult GetAllUserTasks(AllTasksRequest request)
        {
            AllTasksResult result = null;
            try
            {
                result = _taskRepo.GetAllTasks(request);
            }
            catch (Exception ex)
            {
                if (result == null)
                    result = new AllTasksResult();
                result = PopulateErrorMessage(ex, result) as AllTasksResult;
            }
            return result;
        }

        private BaseResult PopulateErrorMessage(Exception ex, BaseResult result)
        {
            result.IsSucceeded = false;
            if (result.Errors == null)
                result.Errors = new List<string>();
            result.Errors.Add(ex.Message);
            return result;
        }
    }
}
