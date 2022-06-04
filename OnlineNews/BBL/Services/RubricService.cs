using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BBL.DTO;
using BBL.Extensions;
using BBL.Services.Interfaces;
using DAL.Repositories;
using DAL.Models;


namespace BBL.Services
{
    public class RubricService : IRubricService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RubricService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RubricDTO> GetByIdAsync(int rubricId)
        {
            var rubric = await _unitOfWork
                .Rubric
                .GetByIdAsync(rubricId);

            return rubric?.ToDto();
        }

        public async Task<IEnumerable<RubricDTO>> GetAllAsync()
        {
            var rubric = await _unitOfWork.Rubric.GetAllAsync();
            var result = rubric.Select(r => r?.ToDto());

            return result;
        }

        public async Task<RubricDTO> CreateAsync(RubricDTO rubricData)
        {
            var rubric = new Rubric
            {
                Id = rubricData.Id,
                Name = rubricData.Name
            };

            await _unitOfWork.Rubric.CreateAsync(rubric);

            return rubric?.ToDto();
        }

        public async Task<bool> UpdateAsync(RubricDTO rubric)
        {
            var inMemoryRubric = await _unitOfWork.Rubric.GetByIdAsync(rubric.Id);

            if (inMemoryRubric == null)
            {
                return false;
            }

            inMemoryRubric.Update(rubric);

            return await _unitOfWork.Rubric.UpdateAsync(inMemoryRubric);
        }

        public async Task<bool> DeleteByIdAsync(int rubricId)
        {
            var rubric = await _unitOfWork.Rubric.GetByIdAsync(rubricId);

            if (rubric == null)
            {
                return false;
            }

            await _unitOfWork.NewsTag.DeleteByIdAsync(rubricId);
            return true;
        }
    }
}
