using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Hotels
{
    public record HotelUpdateCommand(int Id, string Name, string Description, string City, string Address, decimal Commission, bool IsActive) : IRequest<HotelDto>;
}
