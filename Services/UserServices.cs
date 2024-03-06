using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace YoKartApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly YoKartApiDbContext _context;
        private readonly IConfiguration _configuration;

        public UserServices(YoKartApiDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<String> GetUserDetails(LoginUser _login)
        {
            var Isvalidate = _context.Users.Any(s => s.Username == _login.Username && s.Password == _login.Password);
            if (Isvalidate)
            {
                var user = _context.Users.FirstOrDefault(s => s.Username == _login.Username && s.Password == _login.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Username),
                        new Claim("Email", user.Email),
                         new Claim("Roles", user.Roles)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);    

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return tokenString;
                }
              
            }
            return null;
        }
    }
}
