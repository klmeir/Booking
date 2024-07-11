using Booking.Domain.Entities;
using Booking.Domain.Ports;
using Booking.Infrastructure.Ports;
using System.Linq.Expressions;

namespace Booking.Infrastructure.Adapters
{
    [Repository]
    public class RoomRepository : IRoomRepository
    {
        readonly IRepository<Room> _dataSource;

        public RoomRepository(IRepository<Room> dataSource) => _dataSource = dataSource
            ?? throw new ArgumentNullException(nameof(dataSource));

        public async Task<Room> SaveRoom(Room r) => await _dataSource.AddAsync(r);

        public async Task<Room> SingleRoom(int id) => await _dataSource.GetOneAsync(id);

        public async Task UpdateRoom(Room r) => _dataSource.UpdateAsync(r);

        public Task<IEnumerable<Room>> GetManyAsync(Expression<Func<Room, bool>>? filter = null, Func<IQueryable<Room>, IOrderedQueryable<Room>>? orderBy = null, string includeStringProperties = "")
            => _dataSource.GetManyAsync(filter, orderBy, includeStringProperties);
    }
}
