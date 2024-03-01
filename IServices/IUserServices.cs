using YoKartApi.Models;

namespace YoKartApi.IServices
{
    public interface IUserServices
    {
        Task<User> GetUserDetails(List<User> user, LoginUser _login);
    }
}