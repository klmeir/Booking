using Booking.Domain.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Hotels
{
    public class HotelSearchQueryHandler : IRequestHandler<HotelSearchQuery, List<HotelAvailabilityDto>>
    {
        private readonly HotelService _service;

        public HotelSearchQueryHandler(HotelService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<List<HotelAvailabilityDto>> Handle(HotelSearchQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _service.HotelAvailabilityAsync(new SearchAvailability(request.City, request.Guests, request.CheckInDate, request.CheckOutDate), cancellationToken);

            return hotels.Select(h => new HotelAvailabilityDto(h.Id, h.Name, h.Description, h.City, h.Address, 
                h.Rooms.Select(room => new RoomDto(room.Id, room.HotelId, room.BaseCost, room.Taxes, room.RoomType, room.RoomType.ToString(), room.MaxGuests, room.Location, room.IsActive)).ToList())).ToList();
        }
    }
}
