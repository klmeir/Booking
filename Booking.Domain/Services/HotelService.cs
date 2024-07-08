using Booking.Domain.Ports;

namespace Booking.Domain.Services
{
    [DomainService]
    public class HotelService
    {        
        private readonly IHotelRepository _hotelRepository;        

        public HotelService(IHotelRepository paymentResponseRepository)
        {            
            _hotelRepository = paymentResponseRepository;
        }
    }
}
