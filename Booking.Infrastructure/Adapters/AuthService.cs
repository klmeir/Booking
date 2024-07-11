using Booking.Application.Auth;
using Booking.Application.Ports;
using Booking.Infrastructure.Auth;
using Booking.Infrastructure.DataSource.Auth;
using Microsoft.AspNetCore.Identity;

namespace Booking.Infrastructure.Adapters
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        
        public AuthService(UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }
        
        public async Task<LoginResponseDto> LoginUserAsync(LoginCommand login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                return new LoginResponseDto { IsSuccess = true, Token = _tokenGenerator.GenerateToken(user, roles.ToList()) };
            }

            return new LoginResponseDto { IsSuccess = false, Message = "The user does not exist or the credentials are incorrect." };
        }
    }
}
