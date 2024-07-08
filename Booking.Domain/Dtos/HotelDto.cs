namespace Booking.Domain.Dtos
{
    public record HotelDto(int Id, string Name, string Description, string City, string Address, decimal Commission, bool IsActive);
}
