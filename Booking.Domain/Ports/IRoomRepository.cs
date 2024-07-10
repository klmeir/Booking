using Booking.Domain.Entities;
using System.Linq.Expressions;

namespace Booking.Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Room> SaveRoom(Room r);
        Task<Room> SingleRoom(int id);
        Task UpdateRoom(Room r);
        Task<IEnumerable<Room>> GetManyAsync(
            Expression<Func<Room, bool>>? filter = null,
            Func<IQueryable<Room>, IOrderedQueryable<Room>>? orderBy = null,
            string includeStringProperties = "");
    }
}
