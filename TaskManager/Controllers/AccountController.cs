using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.API.Models;
using TaskManager.Core.BusinessServices;
using TaskManager.Core.Domain.BusinessModels;

namespace TaskManager.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IAuthenticationService _authService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authService"></param>
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        [ResponseType(typeof(CreateUserResult))]
        public async Task<IHttpActionResult> Register([FromBody] UserAccountModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.CreateUser(userModel.UserName, userModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok(result);
        }

        private IHttpActionResult GetErrorResult(CreateUserResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.IsSucceeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
