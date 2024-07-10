using Booking.Domain.Entities;

namespace Booking.Domain.Ports
{
    public interface IReservationRepository
    {
        Task<Reservation> SaveReservation(Reservation r);
        Task<Reservation> SingleReservation(int id);
        Task UpdateReservation(Reservation r);
    }
}
