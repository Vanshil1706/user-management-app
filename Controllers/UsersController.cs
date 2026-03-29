using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;


namespace UserManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserByID/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUserByID(id);
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var result = await _repo.CreateUser(user);
            return Ok(result);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var result = await _repo.UpdateUser(id, user);
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _repo.DeleteUser(id);
            return Ok(result);
        }
    }
    }
