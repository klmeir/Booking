using Booking.Domain.Entities;
using Booking.Domain.Ports;
using Booking.Infrastructure.Ports;
using System.Linq.Expressions;

namespace Booking.Infrastructure.Adapters
{
    [Repository]
    public class ReservationRepository : IReservationRepository
    {
        readonly IRepository<Reservation> _dataSource;

        public ReservationRepository(IRepository<Reservation> dataSource) => _dataSource = dataSource
            ?? throw new ArgumentNullException(nameof(dataSource));

        public async Task<Reservation> SaveReservation(Reservation r) => await _dataSource.AddAsync(r);

        public async Task<Reservation> SingleReservation(int id)
        {            
            var query = await _dataSource.GetManyAsync(filter: r => r.Id == id, includeStringProperties: nameof(Reservation.Guests));

            return query.FirstOrDefault();
        }

        public async Task UpdateReservation(Reservation r) => _dataSource.UpdateAsync(r);

        public async Task<bool> CheckAvailability(Expression<Func<Reservation, bool>> filter)
        {
            var query = await _dataSource.GetManyAsync(filter: filter);

            return !query.Any();
        }
    }
}
