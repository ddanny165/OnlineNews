using System.Threading.Tasks;
using System.Collections.Generic;
using BBL.DTO;

namespace BBL.Services.Interfaces
{
    public interface IRubricService
    {
        public Task<RubricDTO> CreateAsync(RubricDTO rubricData);

        public Task<bool> UpdateAsync(RubricDTO rubric);

        public Task<RubricDTO> GetByIdAsync(int Id);

        public Task<IEnumerable<RubricDTO>> GetAllAsync();

        public Task<bool> DeleteByIdAsync(int Id);
    }
}
