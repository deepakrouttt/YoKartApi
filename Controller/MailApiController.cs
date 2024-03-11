using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoKartApi.Mail;
using YoKartApi.Mail.MailServices.IService;

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailApiController : ControllerBase
    {
        private readonly IMailServices _mailService;

        public MailApiController(IMailServices _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMailAsync(mailData);
        }
    }
}
