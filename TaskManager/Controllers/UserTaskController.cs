using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.API.Models;
using TaskManager.Core.BusinessServices;
using TaskManager.Core.Domain.BusinessModels;
using TaskManager.Core.Domain.BusinessModels.Requests;

namespace TaskManager.API.Controllers
{
    [RoutePrefix("api/Task")]
    [Authorize]
    public class UserTaskController : BaseApiController
    {
        private readonly ITasksManagementService _taskService;
        public UserTaskController(ITasksManagementService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ResponseType(typeof(UserTaskApiModel))]
        public IHttpActionResult GetTask([FromUri] int taskId)
        {
            if (taskId <= 0)
                ModelState.AddModelError("UserTaskId", "Invalid Task ID. Task ID should be greater than zero");

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.FindUserTask(new TaskWithIdRequest()
            {
                TaskId = taskId,
                UserId = CurrentUserId
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(new UserTaskApiModel()
            {
                Description = result.Task.Description,
                IsComplete = result.Task.IsComplete,
                UserTaskId = result.Task.UserTaskId
            });
        }

        [HttpGet]
        [ResponseType(typeof(List<UserTaskApiModel>))]
        [Route("All")]
        public IHttpActionResult GetAllTasks()
        {
            var result = _taskService.GetAllUserTasks(new AllTasksRequest()
            {
                UserId = CurrentUserId
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result.Tasks.Select(task => new UserTaskApiModel
            {
                Description = task.Description,
                IsComplete = task.IsComplete,
                UserTaskId = task.UserTaskId
            })
            .ToList());
        }

        [HttpPut]
        [Route("Create")]
        [ResponseType(typeof(List<UserTaskApiModel>))]
        public IHttpActionResult CreateTask([FromBody] UserTaskApiModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.AddUserTask(new TaskRequest()
            {
                Task = new UserTaskModel()
                {
                    Description = task.Description,
                    IsComplete = task.IsComplete
                },
                UserId = CurrentUserId
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result.Tasks.Select(t => new UserTaskApiModel() {
                Description = t.Description,
                UserTaskId = t.UserTaskId,
                IsComplete = t.IsComplete
            }));
        }

        [HttpDelete]
        [Route("Delete")]
        [ResponseType(typeof(List<UserTaskApiModel>))]
        public IHttpActionResult DeleteTask([FromUri] int taskId)
        {
            if (taskId <= 0)
                ModelState.AddModelError("UserTaskId", "Invalid Task ID. Task ID should be greater than zero");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.DeleteUserTask(new TaskWithIdRequest()
            {
                UserId = CurrentUserId,
                TaskId = taskId
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result.Tasks.Select(t => new UserTaskApiModel()
            {
                Description = t.Description,
                UserTaskId = t.UserTaskId,
                IsComplete = t.IsComplete
            }));
        }

        [HttpPost]
        [Route("Update")]
        [ResponseType(typeof(List<UserTaskApiModel>))]
        public IHttpActionResult UpdateTask([FromBody] UserTaskApiModel task)
        {
            if (task.UserTaskId <= 0)
                ModelState.AddModelError("UserTaskId", "Invalid Task ID. Task ID should be greater than zero");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.UpdateTask(new TaskRequest()
            {
                UserId = CurrentUserId,
                Task = new UserTaskModel()
                {
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    UserTaskId = task.UserTaskId
                }
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result.Tasks.Select(t => new UserTaskApiModel()
            {
                Description = t.Description,
                UserTaskId = t.UserTaskId,
                IsComplete = t.IsComplete
            }));
        }
    }
}
