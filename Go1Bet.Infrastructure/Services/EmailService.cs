using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services
{
    public class EmailService
    {
        private static IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            string fromEmail = _configuration["EmailSettings:User"];
            string SMTP = _configuration["EmailSettings:SMTP"];
            int PORT = Int32.Parse(_configuration["EmailSettings:PORT"]);
            string password = _configuration["EmailSettings:Password"];

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.Body = bodyBuilder.ToMessageBody();

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(SMTP, PORT, SecureSocketOptions.Auto);
                smtp.Authenticate(fromEmail, password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }
}
