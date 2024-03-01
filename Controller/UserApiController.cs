using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser _login)
        {
            var Isvalidate = _context.Users.Any(s => s.Username == _login.Username && s.Password == _login.Password);
            if (Isvalidate)
            {
                User user = await _service.GetUserDetails(_context.Users.ToList(), _login);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    foreach (var role in user.Roles.Split(','))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    return Ok(true);
                }
                else
                {
                    return Unauthorized();
                }                                    
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut()
        {
            var username = HttpContext.User.Identity.Name;

            var customClaimValue = HttpContext.User.FindFirst("CustomClaimType")?.Value;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
  
            return Ok("LogOut");
        }

    }
}
