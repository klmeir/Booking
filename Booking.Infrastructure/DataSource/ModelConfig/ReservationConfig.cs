using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entities;

namespace Booking.Infrastructure.DataSource.ModelConfig
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {        
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);

            builder.Property(b => b.HotelId).IsRequired();
            builder.HasIndex(b => b.HotelId);
            builder.Property(b => b.RoomId).IsRequired();
            builder.HasIndex(b => b.RoomId);

            builder.Property(b => b.CheckInDate).IsRequired();
            builder.Property(b => b.CheckOutDate).IsRequired();
            builder.Property(b => b.GuestCount).IsRequired();
            builder.Property(b => b.EmergencyContactName).IsRequired();
            builder.Property(b => b.EmergencyContactPhone).IsRequired();

            builder
                .HasMany(h => h.Guests)
                .WithOne()
                .HasForeignKey(r => r.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
