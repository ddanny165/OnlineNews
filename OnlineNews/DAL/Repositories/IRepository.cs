using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<bool> CreateAsync(TEntity ent);

        Task<bool> UpdateAsync(TEntity ent);

        Task<bool> DeleteByIdAsync(int id);

        Task<int> CountAsync();
    }
}
