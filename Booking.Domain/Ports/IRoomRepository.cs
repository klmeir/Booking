using Booking.Domain.Entities;

namespace Booking.Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Room> SaveRoom(Room r);
        Task<Room> SingleRoom(int id);
    }
}
