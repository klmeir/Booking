using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Hotels
{
    public record HotelAddCommand(string Name, string Description, string City, string Address, decimal Commission) : IRequest<HotelDto>;
}
