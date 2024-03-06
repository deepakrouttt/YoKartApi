using YoKartApi.Models;

namespace YoKartApi.IServices
{
    public interface IUserServices
    {
        Task<String> GetUserDetails(LoginUser _login);
    }
}