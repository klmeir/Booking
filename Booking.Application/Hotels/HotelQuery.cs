using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Hotels
{
    public record HotelQuery(int Id) : IRequest<HotelDto>;
}
