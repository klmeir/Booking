using Booking.Domain.Dtos;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Hotels
{
    public class HotelUpdateCommandHandler : IRequestHandler<HotelUpdateCommand, HotelDto>
    {
        private readonly HotelService _service;

        public HotelUpdateCommandHandler(HotelService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<HotelDto> Handle(HotelUpdateCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _service.SingleHotelAsync(request.Id, cancellationToken);

            hotel.Update(request.Name, request.Description, request.City, request.Address, request.Commission, request.IsActive);

            await _service.UpdateHotelAsync(hotel, cancellationToken);

            return new HotelDto(hotel.Id, hotel.Name, hotel.Description, hotel.City, hotel.Address, hotel.Commission, hotel.IsActive);
        }
    }
}
