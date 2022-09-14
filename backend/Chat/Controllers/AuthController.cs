using ChatProject.Data.Dtos;
using ChatProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        public readonly IAuthService authService;
        public AuthController(IAuthService service)
        {
            this.authService = service;
        }
        [HttpPost, Route("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] UserDto user)
        {
            if(user == null)  
                return BadRequest(); 
            var tokenString = await authService.Login(user);
            return Ok(new TokenDto { Token = tokenString });
        }
    }
}
