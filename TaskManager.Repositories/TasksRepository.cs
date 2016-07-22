using System.Linq;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.BusinessModels.Requests;
using TaskManager.Core.Domain.BusinessModels.Responses;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Repositories;

namespace TaskManager.Repositories
{
    public class TasksRepository : RepositoryBase, ITasksRepository
    {
        public AllTasksResult CreateTask(TaskRequest request)
        {
            var relatedUser = _ctx.Users.Where(user => user.Id.Equals(request.UserId)).First();

            var task = new UserTask()
            {
                Description = request.Task.Description,
                IsComplete = request.Task.IsComplete,
                UserId = request.UserId,
                User = relatedUser
            };

            _ctx.UserTasks.Add(task);
            _ctx.SaveChanges();

            return new AllTasksResult()
            {
                IsSucceeded = true,
                Tasks = _ctx.UserTasks
                        .Where(t => t.UserId.Equals(request.UserId))
                        .Select(tsk => new UserTaskModel()
                        {
                            Description = tsk.Description,
                            IsComplete = tsk.IsComplete,
                            UserTaskId = tsk.UserTaskId
                        }).ToList()
            };
        }

        public AllTasksResult DeleteTask(TaskWithIdRequest request)
        {
            var task = _ctx.UserTasks.Where(t => t.UserTaskId.Equals(request.TaskId) && t.UserId.Equals(request.UserId)).Single();
            if (task != null)
            {
                _ctx.UserTasks.Remove(task);
                _ctx.SaveChanges();
            }
            return new AllTasksResult()
            {
                IsSucceeded = true,
                Tasks = _ctx.UserTasks
                        .Where(t => t.UserId.Equals(request.UserId))
                        .Select(tsk => new UserTaskModel()
                        {
                            Description = tsk.Description,
                            IsComplete = tsk.IsComplete,
                            UserTaskId = tsk.UserTaskId
                        }).ToList()
            };
        }

        public AllTasksResult GetAllTasks(AllTasksRequest request)
        {
            var tasks = _ctx.UserTasks.Where(task => task.UserId.Equals(request.UserId));
            return new AllTasksResult()
            {
                IsSucceeded = true,
                Tasks = _ctx.UserTasks
                        .Where(t => t.UserId.Equals(request.UserId))
                        .Select(tsk => new UserTaskModel()
                        {
                            Description = tsk.Description,
                            IsComplete = tsk.IsComplete,
                            UserTaskId = tsk.UserTaskId
                        }).ToList()
            };
        }

        public TaskResult GetTask(TaskWithIdRequest request)
        {
            var task = _ctx.UserTasks
                .Where(t => t.UserTaskId.Equals(request.TaskId) && t.UserId.Equals(request.UserId))
                .Select(tsk => new UserTaskModel()
                {
                    Description = tsk.Description,
                    IsComplete = tsk.IsComplete,
                    UserTaskId = tsk.UserTaskId
                }).Single();

            return new TaskResult()
            {
                IsSucceeded = true,
                Task = task
            };
        }

        public AllTasksResult UpdateTask(TaskRequest request)
        {
            var task = _ctx.UserTasks.Where(t => t.UserTaskId.Equals(request.Task.UserTaskId) && t.UserId.Equals(request.UserId)).SingleOrDefault();

            if (task != null)
            {
                var relatedUser = _ctx.Users.Where(user => user.Id.Equals(request.UserId)).First();

                _ctx.Entry<UserTask>(task)
                .CurrentValues.SetValues(new UserTask()
                {
                    Description = request.Task.Description,
                    IsComplete = request.Task.IsComplete,
                    UserTaskId = request.Task.UserTaskId,
                    UserId = request.UserId,
                    User = relatedUser
                });

                _ctx.SaveChanges();
            }

            return new AllTasksResult()
            {
                IsSucceeded = true,
                Tasks = _ctx.UserTasks
                        .Where(t => t.UserId.Equals(request.UserId))
                        .Select(tsk => new UserTaskModel()
                        {
                            Description = tsk.Description,
                            IsComplete = tsk.IsComplete,
                            UserTaskId = tsk.UserTaskId
                        }).ToList()
            };
        }
    }
}
