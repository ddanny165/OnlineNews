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
    public class TagService : ITagService
    { 
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TagDTO> GetByIdAsync(int tagId)
        {
            var tag = await _unitOfWork
                .Tag
                .GetByIdAsync(tagId);

            return tag?.ToDto();
        }

        public async Task<IEnumerable<TagDTO>> GetAllAsync()
        {
            var tags = await _unitOfWork.Tag.GetAllAsync();
            var result = tags.Select(t => t?.ToDto());

            return result;
        }

        public async Task<TagDTO> CreateAsync(TagDTO tagData)
        {
            var tag = new Tag
            {
                Id = tagData.Id,
                Name = tagData.Name
            };

            await _unitOfWork.Tag.CreateAsync(tag);

            return tag?.ToDto();
        }

        public async Task<bool> UpdateAsync(TagDTO tag)
        {
            var inMemoryTag = await _unitOfWork.Tag.GetByIdAsync(tag.Id);

            if (inMemoryTag == null)
            {
                return false;
            }

            inMemoryTag.Update(tag);

            return await _unitOfWork.Tag.UpdateAsync(inMemoryTag);
        }

        public async Task<bool> DeleteByIdAsync(int tagId)
        {
            var tag = await _unitOfWork.Tag.GetByIdAsync(tagId);

            if (tag == null)
            {
                return false;
            }

            await _unitOfWork.Tag.DeleteByIdAsync(tagId);
            return true;
        }
    }
}
