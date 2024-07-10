using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Hotels
{
    public record HotelSearchQuery(string City, int Guests, string CheckInDate, string CheckOutDate) : IRequest<List<HotelAvailabilityDto>>;
}
