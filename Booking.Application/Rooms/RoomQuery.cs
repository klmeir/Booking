using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Hotels
{
    public record RoomQuery(int Id) : IRequest<RoomDto>;
}
