using Booking.Domain.Dtos;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Hotels
{
    public class HotelQueryHandler : IRequestHandler<HotelQuery, HotelDto>
    {
        private readonly HotelService _service;

        public HotelQueryHandler(HotelService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<HotelDto> Handle(HotelQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _service.SingleHotelAsync(request.Id, cancellationToken);

            return new HotelDto(hotel.Id, hotel.Name, hotel.Description, hotel.City, hotel.Address, hotel.Commission, hotel.IsActive);
        }
    }
}
