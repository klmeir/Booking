using Booking.Domain.Entities;

namespace Booking.Domain.Ports
{
    public interface IHotelRepository
    {
        Task<Hotel> SaveHotel(Hotel h);
        Task<Hotel> SingleHotel(int id);
    }
}
