using Focus.Business.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Focus.Business.EmailConfigurationSetup.Model;

namespace Focus.Business
{
    public interface ISendEmail
    {
        Task SendEmailAsync(SendEmailDto emailMessage, string UserName, string Id, string appLink);
        Task SendMailMessage(SendEmailDto emailMessage, EmailConfigurationLookupModel emailConfiguration);
        Task SendInvoiceEmailLinkAsync(EmailCompose emailCompose);
        Task SendInvoiceEmailPdfAsync(EmailCompose emailCompose);
    }
}
