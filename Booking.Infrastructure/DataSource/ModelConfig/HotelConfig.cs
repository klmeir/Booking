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

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.City).IsRequired();
            builder.Property(b => b.Address).IsRequired();
            builder.Property(b => b.Commission).IsRequired();

            builder
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(h => h.Reservations)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
