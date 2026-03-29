using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _repo.Login(request);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _repo.Logout();

            return Ok("Logout successful");
        }
    }
}