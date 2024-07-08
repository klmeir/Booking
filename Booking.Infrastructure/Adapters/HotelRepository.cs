using Booking.Domain.Entities;
using Booking.Domain.Ports;
using Booking.Infrastructure.Ports;

namespace Booking.Infrastructure.Adapters
{
    [Repository]
    public class HotelRepository : IHotelRepository
    {
        readonly IRepository<Hotel> _dataSource;

        public HotelRepository(IRepository<Hotel> dataSource) => _dataSource = dataSource
            ?? throw new ArgumentNullException(nameof(dataSource));

        public async Task<Hotel> SaveHotel(Hotel h) => await _dataSource.AddAsync(h);

        public async Task<Hotel> SingleHotel(int id) => await _dataSource.GetOneAsync(id);


    }
}
