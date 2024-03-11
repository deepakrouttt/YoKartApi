using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;
        private readonly IUserServices _service;

        public UserApiController(YoKartApiDbContext context, IUserServices service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();

            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var users = _context.Users.Find(id);

            return Ok(users);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser _login)
        {
            if (string.IsNullOrEmpty(_login.Username) || string.IsNullOrEmpty(_login.Password))
                throw new Exception("Credentials are not valid");

            var userData = await _service.GetUserDetails(_login);

            if (userData != null)
            {
                return Ok(userData);
            }
            if (userData == null)
                throw new Exception("User is not valid");
            return Unauthorized();
        }

    }
}
