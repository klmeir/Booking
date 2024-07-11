using Booking.Application.Auth;
using Booking.Infrastructure.DataSource.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Booking.Infrastructure.DataSource
{
    public class ContextInitialize
    {
        protected ContextInitialize() { }

        public static void Seed(IServiceProvider serviceProvider)
        {
            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = serviceProvider.CreateScope())
            {

                try
                {                    
                    var context = scope.ServiceProvider.GetService<DataContext>();

                    context.Database.Migrate();

                    // for testing purposes                    
                    SeedUsers(scope.ServiceProvider);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ContextInitialize>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

            }

        }

        private static void SeedUsers(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<ContextInitialize>>();
            var rolAgency = RoleEnum.Agency.ToString();
            var rolTraveler = RoleEnum.Traveler.ToString();
            var emailAgency = "agency.admin@booking.com";
            var emailGuest = "guest1@gmail.com";


            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any())
            {
                var result1 = roleManager.CreateAsync(new IdentityRole
                {
                    Name = rolAgency,
                }).Result;

                var result2 = roleManager.CreateAsync(new IdentityRole
                {
                    Name = rolTraveler,
                }).Result;

                if (!result1.Succeeded || !result2.Succeeded)
                {
                    logger.LogError("An error occurred while setting up roles.");
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (!userManager.Users.Any())
            {
                var result1 = userManager.CreateAsync(new ApplicationUser
                {                    
                    Email = emailAgency,     
                    UserName = emailAgency
                }, "PassAgency2024*").Result;

                var result2 = userManager.CreateAsync(new ApplicationUser
                {                    
                    Email = emailGuest,     
                    UserName = emailGuest,
                }, "PassGuest2024*").Result;

                if (!result1.Succeeded || !result2.Succeeded)
                {
                    logger.LogError("An error occurred while setting up users.");
                }
                
                var result3 = userManager.AddToRoleAsync(userManager.FindByEmailAsync(emailAgency).Result, rolAgency).Result;
                var result4 = userManager.AddToRoleAsync(userManager.FindByEmailAsync(emailGuest).Result, rolTraveler).Result;

                if (!result3.Succeeded || !result4.Succeeded)
                {
                    logger.LogError("An error occurred while setting up users roles.");
                }
            }
        }

    }
}
