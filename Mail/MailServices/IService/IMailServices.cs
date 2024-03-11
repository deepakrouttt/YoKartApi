using YoKartApi.Mail;

namespace YoKartApi.Mail.MailServices.IService
{
    public interface IMailServices
    {
        Task<bool> SendMailAsync(MailData mailData);
    }
}