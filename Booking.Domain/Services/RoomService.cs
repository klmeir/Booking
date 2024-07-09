using Booking.Domain.Entities;
using Booking.Domain.Exception;
using Booking.Domain.Ports;

namespace Booking.Domain.Services
{
    [DomainService]
    public class RoomService
    {        
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HotelService _hotelService;

        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork, HotelService hotelService) =>
            (_roomRepository, _unitOfWork, _hotelService) = (roomRepository, unitOfWork, hotelService);

        public async Task<Room> SaveRoomAsync(Room r, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            await _hotelService.CheckIfExistsHotelAsync(r.HotelId, token);

            var returnRoom = await _roomRepository.SaveRoom(r);
            await _unitOfWork.SaveAsync(token);
            return returnRoom;
        }

        public async Task<Room> SingleRoomAsync(int id, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            return await _roomRepository.SingleRoom(id) ?? throw new NotFoundException("The specified room could not be found"); ;
        }

        public async Task<Room> UpdateRoomAsync(Room r, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;
            await _roomRepository.UpdateRoom(r);
            await _unitOfWork.SaveAsync(token);

            return r;
        }
    }
}
