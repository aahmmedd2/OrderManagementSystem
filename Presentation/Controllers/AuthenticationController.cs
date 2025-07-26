using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("register")] // POST /api/users/register
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var Token = await _serviceManager.AuthenticationService.RegisterAsync(userRegisterDto);

                return Ok(new { Token });
            } 
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")] // POST /api/users/login
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var Token = await _serviceManager.AuthenticationService.LoginAsync(userLoginDto);

                return Ok(new { Token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
