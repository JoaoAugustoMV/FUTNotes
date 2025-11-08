using FootNotes.Core.Data.Communication;
using FootNotes.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.Core.Data
{
    public class RepositoryBase<TEntity, TDbContext>(TDbContext dbContext, IMediatorHandler mediatorHandler):
        IRepositoryBase<TEntity> where TEntity : Entity, IAggregateRoot
        where TDbContext: DbContext
     
    {
        protected TDbContext DbContext => dbContext;
        private async Task<bool> Commit()
        {
            var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            bool save = await dbContext.SaveChangesAsync() > 0;

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediatorHandler.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);

            return save;
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);

            await Commit();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbContext.AddRangeAsync(entities);
            await Commit();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            await dbContext.AddRangeAsync(entities);
            await Commit();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dbContext.Remove(entity);

            await Commit();
        }      

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {            
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();            
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbContext.Update(entity);

            await Commit();
        }

        public async Task<bool> ExistsId(Guid id)
        {
            return await dbContext.Set<TEntity>().AnyAsync(e => e.Id == id);            
        }
    }
}
