using Booking.Domain.Dtos;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Rooms
{
    public record RoomUpdateCommand(int Id, int HotelId, decimal BaseCost, decimal Taxes, RoomTypeEnum RoomType, int MaxGuests, string Location, bool IsActive) : IRequest<RoomDto>;
}
