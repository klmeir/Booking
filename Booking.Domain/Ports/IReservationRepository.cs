using Booking.Domain.Entities;
using System.Linq.Expressions;

namespace Booking.Domain.Ports
{
    public interface IReservationRepository
    {
        Task<Reservation> SaveReservation(Reservation r);
        Task<IEnumerable<Reservation>> AllReservation();
        Task<Reservation> SingleReservation(int id);
        Task UpdateReservation(Reservation r);
        Task<bool> CheckAvailability(Expression<Func<Reservation, bool>> filter);
    }
}
