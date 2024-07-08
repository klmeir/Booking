using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entities;
using Booking.Infrastructure.DataSource;
using Booking.Infrastructure.Ports;

namespace Booking.Infrastructure.Adapters
{
    public class GenericRepository<T> : IRepository<T> where T : DomainEntity
    {
        readonly DataContext Context;
        readonly DbSet<T> _dataset;

        public GenericRepository(DataContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _dataset = Context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
            await _dataset.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

    }
}
