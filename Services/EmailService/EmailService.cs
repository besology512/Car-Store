using Car_Store.models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Car_Store.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;
        public EmailService(IConfiguration config)
        {
            this.config = config;
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("18035@stemegypt.edu.eg"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            smtp.Authenticate("18035@stemegypt.edu.eg", "badabidorashad");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
