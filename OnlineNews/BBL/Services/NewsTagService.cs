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
    public class NewsTagService : INewsTagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NewsTagDTO> GetByIdAsync(int newsTagId)
        {
            var newsTag = await _unitOfWork
                .NewsTag
                .GetByIdAsync(newsTagId);

            return newsTag?.ToDto();
        }

        public async Task<IEnumerable<NewsTagDTO>> GetAllAsync()
        {
            var newsTags = await _unitOfWork.NewsTag.GetAllAsync();
            var result = newsTags.Select(nt => nt?.ToDto());

            return result;
        }

        public async Task<NewsTagDTO> CreateAsync(NewsTagDTO newsData)
        {
            var newsTag = new NewsTag
            {
                Id = newsData.Id,
                NewsId = newsData.NewsId,
                TagId = newsData.TagId
            };

            await _unitOfWork.NewsTag.CreateAsync(newsTag);

            return newsTag?.ToDto();
        }

        public async Task<bool> UpdateAsync(NewsTagDTO newsTag)
        {
            var inMemoryNewsTag = await _unitOfWork.NewsTag.GetByIdAsync(newsTag.Id);

            if (inMemoryNewsTag == null)
            {
                return false;
            }

            inMemoryNewsTag.Update(newsTag);

            return await _unitOfWork.NewsTag.UpdateAsync(inMemoryNewsTag);
        }

        public async Task<bool> DeleteByIdAsync(int newsTagId)
        {
            var newsTag = await _unitOfWork.NewsTag.GetByIdAsync(newsTagId);

            if (newsTag == null)
            {
                return false;
            }

            await _unitOfWork.NewsTag.DeleteByIdAsync(newsTagId);
            return true;
        }
    }
}
