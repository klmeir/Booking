using Booking.Domain.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Hotels
{
    public class HotelAddCommandHandler : IRequestHandler<HotelAddCommand, HotelDto>
    {
        private readonly HotelService _service;

        public HotelAddCommandHandler(HotelService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<HotelDto> Handle(HotelAddCommand request, CancellationToken cancellationToken)
        {
            var hotelSaved = await _service.SaveHotelAsync(
                new Hotel(request.Name, request.Description, request.City, request.Address, request.Commission), cancellationToken
            );

            return new HotelDto(hotelSaved.Id, hotelSaved.Name, hotelSaved.Description, hotelSaved.City, hotelSaved.Address, hotelSaved.Commission, hotelSaved.IsActive);
        }
    }
}
