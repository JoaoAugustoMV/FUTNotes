using FootNotes.Core.Data.Communication;
using FootNotes.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.Core.Data
{
    public class RepositoryBase<T>(DbContext _dbContext, IMediatorHandler mediatorHandler) : IRepositoryBase<T> where T : Entity, IAggregateRoot
    {
        private async Task<bool> Commit()
        {
            var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediatorHandler.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);

            await Commit();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);

            await Commit();
        }      

        public async Task<T?> GetByIdAsync(Guid id)
        {            
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();            
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);

            await Commit();
        }

        public async Task<bool> ExistsId(Guid id)
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);            
        }
    }
}
