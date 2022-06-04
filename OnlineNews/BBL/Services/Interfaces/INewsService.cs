using System.Threading.Tasks;
using System.Collections.Generic;
using BBL.DTO;

namespace BBL.Services.Interfaces
{
    public interface INewsService
    {
        public Task<NewsDTO> CreateAsync(NewsDTO newsData);

        public Task<bool> UpdateAsync(NewsDTO news);

        public Task<NewsDTO> GetByIdAsync(int Id);

        public Task<IEnumerable<NewsDTO>> GetAllAsync();

        public Task<bool> DeleteByIdAsync(int Id);
    }
}
