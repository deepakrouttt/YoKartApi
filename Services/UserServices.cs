using Microsoft.EntityFrameworkCore;
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

            return _user;
        }
    }
}
