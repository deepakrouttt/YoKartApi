using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoKartApi.Data;
using YoKartApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;

        public UserApiController(YoKartApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();

            return Ok(users);
        }

        [HttpPost]
        public string Login ([FromBody] LoginUser _login)
        {

            if (string.IsNullOrEmpty(_login.Username) || string.IsNullOrEmpty(_login.Password))
                throw new Exception("Credentials are not valid");

            var user = _context.Users.SingleOrDefaultAsync(s => s.Username == _login.Username && s.Password == _login.Password);

            if (user == null)
                throw new Exception("User is not valid");

            return _login.Password;
        }

    }
}
