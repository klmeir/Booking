using Booking.Domain.Dtos;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Rooms
{
    public record RoomAddCommand(int HotelId, decimal BaseCost, decimal Taxes, RoomTypeEnum RoomType, int MaxGuests, string Location) : IRequest<RoomDto>;
}
