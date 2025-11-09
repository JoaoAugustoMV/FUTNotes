using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Core.Data
{
    public interface IRepositoryBase<T> where T : IAggregateRoot
    {
        //IUnitOfWork UnitOfWork { get; }
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> ListAsync();
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task AddRangeAsync<TEntityAgreggate>(IEnumerable<TEntityAgreggate> entities) where TEntityAgreggate : Entity;
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<bool> ExistsId(Guid id);


    }
}
