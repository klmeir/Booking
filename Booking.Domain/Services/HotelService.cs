using Booking.Domain.Entities;
using Booking.Domain.Exception;
using Booking.Domain.Ports;
using System.Globalization;

namespace Booking.Domain.Services
{
    [DomainService]
    public class HotelService
    {        
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HotelService(IHotelRepository hotelRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Hotel> SaveHotelAsync(Hotel h, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;
            var returnHotel = await _hotelRepository.SaveHotel(h);
            await _unitOfWork.SaveAsync(token);
            return returnHotel;
        }

        public async Task<Hotel> SingleHotelAsync(int id, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            return await _hotelRepository.SingleHotel(id) ?? throw new NotFoundException("The specified hotel could not be found"); ;
        }

        public async Task CheckIfExistsHotelAsync(int id, CancellationToken? cancellationToken = null)
        {
            var hotel = await _hotelRepository.SingleHotel(id);

            if (hotel is null)
                throw new CoreBusinessException("The specified hotel could not be found");
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel h, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;
            await _hotelRepository.UpdateHotel(h);
            await _unitOfWork.SaveAsync(token);

            return h;
        }

        public async Task<IEnumerable<Hotel>> HotelAvailabilityAsync(SearchAvailability searchAvailability, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            string DateFormat = "yyyy-MM-dd";
            DateTime.TryParseExact(searchAvailability.CheckInDate, DateFormat, null, DateTimeStyles.None, out var checkInDateDt);
            DateTime.TryParseExact(searchAvailability.CheckOutDate, DateFormat, null, DateTimeStyles.None, out var checkOutDateDt);

            var checkInDate = new DateOnly(checkInDateDt.Year, checkInDateDt.Month, checkInDateDt.Day);
            var checkOutDate = new DateOnly(checkOutDateDt.Year, checkOutDateDt.Month, checkOutDateDt.Day);

            var rooms = await _roomRepository.GetManyAsync(filter: room =>
                room.Hotel.IsActive &&
                room.Hotel.City == searchAvailability.City &&
                room.IsActive &&
                room.MaxGuests >= searchAvailability.Guests &&
                !room.Reservations.Any(reservation =>
                    reservation.CheckInDate <= checkOutDate &&
                    reservation.CheckOutDate >= checkInDate),
                    includeStringProperties: nameof(Room.Hotel));

            var hotelsWithRooms = rooms
                .GroupBy(room => room.Hotel)
                .Select(group => group.Key)
                .ToList();

            return hotelsWithRooms;
        }
    }
}
