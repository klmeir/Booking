using Booking.Domain.Enums;

namespace Booking.Domain.Dtos
{
    public record RoomDto(int Id, int HotelId, decimal BaseCost, decimal Taxes, RoomTypeEnum RoomType, string Location, bool IsActive);
}
