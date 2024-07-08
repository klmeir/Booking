using Booking.Domain.Entities;

namespace Booking.Domain.Ports
{
    public interface IHotelRepository
    {
        Task<Hotel> SaveXml(Hotel hotel);
    }
}
