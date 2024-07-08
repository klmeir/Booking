using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Persons
{
    public record HotelAddCommand(string Name, string Description, string City, string Address, decimal Commission) : IRequest<HotelDto>;
}
