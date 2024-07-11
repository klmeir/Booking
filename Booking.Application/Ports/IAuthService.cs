using Booking.Application.Auth;

namespace Booking.Application.Ports
{
    public interface IAuthService
    {        
        Task<LoginResponseDto> LoginUserAsync(LoginCommand login);
    }
}
