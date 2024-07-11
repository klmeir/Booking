using Booking.Application.Auth;
using Booking.Application.Ports;
using Booking.Infrastructure.Adapters;
using Booking.Infrastructure.Auth;
using Booking.Infrastructure.DataSource;
using Booking.Infrastructure.DataSource.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Booking.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyEnum.AgencyOrTravelerPolicy.ToString(), policy =>
                       policy
                           .RequireRole(RoleEnum.Agency.ToString(), RoleEnum.Traveler.ToString()))
                 .AddPolicy(PolicyEnum.AgencyPolicy.ToString(), policy =>
                       policy
                           .RequireRole(RoleEnum.Agency.ToString()))
                 .AddPolicy(PolicyEnum.TravelerPolicy.ToString(), policy =>
                       policy
                           .RequireRole(RoleEnum.Traveler.ToString()));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"] ?? string.Empty)),
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }
    }
}
