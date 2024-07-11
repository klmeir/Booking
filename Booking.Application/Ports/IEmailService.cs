namespace Booking.Application.Ports
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request, CancellationToken ct);
    }
}
