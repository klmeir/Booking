using Booking.Domain.Dtos;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Hotels
{
    public class RoomQueryHandler : IRequestHandler<RoomQuery, RoomDto>
    {
        private readonly RoomService _service;

        public RoomQueryHandler(RoomService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<RoomDto> Handle(RoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _service.SingleRoomAsync(request.Id, cancellationToken);

            return new RoomDto(room.Id, room.HotelId, room.BaseCost, room.Taxes, room.RoomType, room.RoomType.ToString(), room.MaxGuests, room.Location, room.IsActive);
        }
    }
}
