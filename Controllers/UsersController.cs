using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreSwap.Models;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DotNetCoreSwap.Controllers
{
    /// <summary>
    /// Users controller.
    /// </summary>
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private IUserService _userService;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="service">Service.</param>
		public UsersController(IUserService service)
		{
			_userService = service;
		}

        /// <summary>
        /// Authenticate the specified user.
        /// </summary>
        /// <returns>The authenticated user BASE64 token</returns>
        /// <param name="userDetails">User request</param>
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public async  Task<IActionResult> Authenticate([FromBody]UserRequest userDetails)
		{
			var user = await _userService.Authenticate(userDetails.Username, userDetails.Password);

			if (user == null)
				return BadRequest(new { message = "Username of password is incorrect"});

			return Ok(new JsonResult(user.Base64));
		}

        /// <summary>
        /// Returns all the users
        /// </summary>
        /// <returns>User list</returns>
		[HttpGet("getallusers")]
		[SwaggerResponse(200, Type = typeof(List<UserResponse>))]
		[SwaggerResponse(400, Description = "Invalid", Type = typeof(Exception))]
		[SwaggerResponse(401, Description = "Not Auth", Type = typeof(Exception))]
		[SwaggerResponse(403, Description = "Not Au", Type = typeof(Exception))]

        public async Task<IActionResult> GetAll()
		{
			var users = await _userService.GetAll();
			return Ok(users);
		}
    }
}