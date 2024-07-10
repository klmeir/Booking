namespace Booking.Domain.Dtos
{
    public record HotelAvailabilityDto(int Id, string Name, string Description, string City, string Address, List<RoomDto> Rooms);
}
