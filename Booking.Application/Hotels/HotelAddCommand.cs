using Booking.Domain.Entities;
using MediatR;

namespace Booking.Application.Persons
{
    public record HotelAddCommand(int Id) : IRequest<Hotel>;
}
