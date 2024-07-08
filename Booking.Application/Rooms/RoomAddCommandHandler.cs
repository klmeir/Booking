using Booking.Domain.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Rooms
{
    public class RoomAddCommandHandler : IRequestHandler<RoomAddCommand, RoomDto>
    {
        private readonly RoomService _service;

        public RoomAddCommandHandler(RoomService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<RoomDto> Handle(RoomAddCommand request, CancellationToken cancellationToken)
        {
            var roomSaved = await _service.SaveRoomAsync(
            new Room(request.HotelId, request.BaseCost, request.Taxes, request.RoomType, request.Location), cancellationToken
        );

            return new RoomDto(roomSaved.Id, roomSaved.HotelId, roomSaved.BaseCost, roomSaved.Taxes, roomSaved.RoomType, roomSaved.Location, roomSaved.IsActive);
        }
    }
}
