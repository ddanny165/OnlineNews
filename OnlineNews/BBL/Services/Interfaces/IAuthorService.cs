using System.Threading.Tasks;
using System.Collections.Generic;
using BBL.DTO;

namespace BBL.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorDTO> CreateAsync(AuthorDTO authorData);

        public Task<bool> UpdateAsync(AuthorDTO author);

        public Task<AuthorDTO> GetByIdAsync(int Id);

        public Task<IEnumerable<AuthorDTO>> GetAllAsync();

        public Task<bool> DeleteByIdAsync(int Id);
    }
}
