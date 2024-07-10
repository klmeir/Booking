using Booking.Domain.Enums;

namespace Booking.Domain.Dtos
{
    public record GuestDto(string Name, string Birthdate, GenderEnum Gender, DocumentTypeEnum DocumentType, string DocumentNumber, string Email, string Phone);
}
