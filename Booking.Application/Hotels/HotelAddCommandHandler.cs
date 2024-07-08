using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Persons
{
    public class HotelAddCommandHandler : IRequestHandler<HotelAddCommand, Hotel>
    {
        private readonly HotelService _service;

        public HotelAddCommandHandler(HotelService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<Hotel> Handle(HotelAddCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
