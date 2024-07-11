using MediatR;

namespace Booking.Application.Auth
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto>;
}
