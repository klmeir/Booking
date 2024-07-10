using Booking.Application.Ports;
using Booking.Domain.Exception;
using Booking.Infrastructure.Mailing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Booking.Infrastructure.Adapters
{
    public class SmtpEmailService : IEmailService
    {
        private readonly MailSettings _settings;

        public SmtpEmailService(IOptions<MailSettings> settings) =>
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

        public async Task SendAsync(EmailRequest request, CancellationToken ct)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(request.From ?? _settings.From),
                    Subject = request.Subject,
                    Body = request.Body,
                    IsBodyHtml = false
                };
                message.Sender = new MailAddress(request.From ?? _settings.From, request.DisplayName ?? _settings.DisplayName);
                request.To.ForEach(message.To.Add);
                request.Cc.ForEach(message.CC.Add);

                using var _smtpClient = new SmtpClient(_settings.Host, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
                    EnableSsl = true
                };
                await _smtpClient.SendMailAsync(message);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw new CoreBusinessException("Failed to send email");
            }
        }
    }
}
