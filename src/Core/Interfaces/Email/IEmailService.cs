using System.Threading.Tasks;

namespace AzureFunctions.Core.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string toName, string subject, string message);
    }
}
