using Booking.Domain.Entities;

namespace Booking.Infrastructure.Ports
{
    public interface IRepository<T> where T : DomainEntity
    {
        Task<T> AddAsync(T entity);

    }
}
