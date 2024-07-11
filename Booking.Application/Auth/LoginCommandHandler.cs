using Booking.Application.Ports;
using MediatR;

namespace Booking.Application.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAuthService _authService;
        
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            return await _authService.LoginUserAsync(request).ConfigureAwait(false);
        }
    }
}
