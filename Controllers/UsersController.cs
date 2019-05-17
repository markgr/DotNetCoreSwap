using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Models;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace DotNetCoreSwap.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private IUserService _userService;

		public UsersController(IUserService service)
		{
			_userService = service;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public async  Task<IActionResult> Authenticate([FromBody]UserRequest userDetails)
		{
			var user = await _userService.Authenticate(userDetails.Username, userDetails.Password);

			if (user == null)
				return BadRequest(new { message = "Username of password is incorrect"});

			return Ok(new JsonResult(user.Base64));
		}

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