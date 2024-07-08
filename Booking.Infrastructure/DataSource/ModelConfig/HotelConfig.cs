using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entities;

namespace Booking.Infrastructure.DataSource.ModelConfig
{
    public class HotelConfig : IEntityTypeConfiguration<Hotel>
    {        
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);
        }
    }
}
