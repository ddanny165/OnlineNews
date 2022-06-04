using System.Threading.Tasks;
using System.Collections.Generic;
using BBL.DTO;

namespace BBL.Services.Interfaces
{
    public interface ITagService
    {
        public Task<TagDTO> CreateAsync(TagDTO tagData);

        public Task<bool> UpdateAsync(TagDTO tag);

        public Task<TagDTO> GetByIdAsync(int Id);

        public Task<IEnumerable<TagDTO>> GetAllAsync();

        public Task<bool> DeleteByIdAsync(int Id);
    }
}
