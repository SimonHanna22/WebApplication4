using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication4.Utility
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic email for now
            return Task.CompletedTask;
        }
    }
}
