using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entities;

namespace Booking.Infrastructure.DataSource.ModelConfig
{
    public class GuestConfig : IEntityTypeConfiguration<Guest>
    {        
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);

            builder.Property(b => b.ReservationId).IsRequired();
            builder.HasIndex(b => b.ReservationId);

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Birthdate).IsRequired();
            builder.Property(b => b.Gender).IsRequired();
            builder.Property(b => b.DocumentType).IsRequired();
            builder.Property(b => b.DocumentNumber).IsRequired();
            builder.Property(b => b.Email).IsRequired();
            builder.Property(b => b.Phone).IsRequired();
        }
    }
}
