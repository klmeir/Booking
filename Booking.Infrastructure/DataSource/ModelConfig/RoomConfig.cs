using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entities;

namespace Booking.Infrastructure.DataSource.ModelConfig
{
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {        
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);

            builder.Property(b => b.HotelId).IsRequired();
            builder.HasIndex(b => b.HotelId);

            builder.Property(b => b.BaseCost).IsRequired();
            builder.Property(b => b.Taxes).IsRequired();
            builder.Property(b => b.RoomType).IsRequired();
            builder.Property(b => b.MaxGuests).IsRequired();
            builder.Property(b => b.Location).IsRequired();
            builder.Property(b => b.IsActive).IsRequired();

            builder
                .HasMany(h => h.Reservations)
                .WithOne(r => r.Room)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
