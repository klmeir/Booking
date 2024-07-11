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
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ContextInitialize>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

            }

        }

    }
}
