using System.Threading.Tasks;
using System.Collections.Generic;
using BBL.DTO;

namespace BBL.Services.Interfaces
{
    public interface INewsTagService
    {
        public Task<NewsTagDTO> CreateAsync(NewsTagDTO newsTagsData);

        public Task<bool> UpdateAsync(NewsTagDTO newsTags);

        public Task<NewsTagDTO> GetByIdAsync(int Id);

        public Task<IEnumerable<NewsTagDTO>> GetAllAsync();

        public Task<bool> DeleteByIdAsync(int Id);
    }
}
