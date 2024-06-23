using RMS.Business.Helpers.Email;

namespace RMS.Business.Services.Abstracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
