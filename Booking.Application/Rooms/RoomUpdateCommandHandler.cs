using Booking.Domain.Dtos;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Rooms
{
    public class RoomUpdateCommandHandler : IRequestHandler<RoomUpdateCommand, RoomDto>
    {
        private readonly RoomService _service;

        public RoomUpdateCommandHandler(RoomService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<RoomDto> Handle(RoomUpdateCommand request, CancellationToken cancellationToken)
        {         
            var room = await _service.SingleRoomAsync(request.Id, cancellationToken);

            room.Update(request.HotelId, request.BaseCost, request.Taxes, request.RoomType, request.Location, request.IsActive);

            await _service.UpdateRoomAsync(room, cancellationToken);

            return new RoomDto(room.Id, room.HotelId, room.BaseCost, room.Taxes, room.RoomType, room.RoomType.ToString(), room.Location, room.IsActive);
        }
    }
}
