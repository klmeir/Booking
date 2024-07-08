using Booking.Domain.Entities;
using Booking.Domain.Ports;

namespace Booking.Domain.Services
{
    [DomainService]
    public class HotelService
    {        
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HotelService(IHotelRepository hotelRepository, IUnitOfWork unitOfWork) =>
            (_hotelRepository, _unitOfWork) = (hotelRepository, unitOfWork);

        public async Task<Hotel> SaveHotelAsync(Hotel h, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;
            var returnHotel = await _hotelRepository.SaveHotel(h);
            await _unitOfWork.SaveAsync(token);
            return returnHotel;
        }
    }
}
