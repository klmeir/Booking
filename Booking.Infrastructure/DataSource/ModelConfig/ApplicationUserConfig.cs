using Booking.Infrastructure.DataSource.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.DataSource.ModelConfig
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {        
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Users");
        }
    }
}
