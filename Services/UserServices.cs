using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;

namespace YoKartApi.Services
{
    public class UserServices : IUserServices
    {
        public async Task<User> GetUserDetails(List<User> user, LoginUser _login)
        {
            var _user = user.FirstOrDefault(s => s.Username == _login.Username && s.Password == _login.Password);

            //var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, user.Username),
            //            new Claim(ClaimTypes.Email, user.Email),
            //        };

            //foreach (var role in user.Roles.Split(','))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
            //}

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);

            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //};

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return _user;
        }
    }
}
