using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Web.Http.Results;
using TaskManager.Core.Domain.BusinessModels;

namespace TaskManager.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected string CurrentUserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        protected InvalidModelStateResult BadRequest(BaseResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return BadRequest(ModelState);
        }
    }
}
