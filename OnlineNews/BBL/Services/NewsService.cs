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
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NewsDTO> GetByIdAsync(int newsId)
        {
            var news = await _unitOfWork
                .News
                .GetByIdAsync(newsId);

            return news?.ToDto();
        }

        public async Task<IEnumerable<NewsDTO>> GetAllAsync()
        {
            var news = await _unitOfWork.News.GetAllAsync();
            var result = news.Select(n => n?.ToDto());

            return result;
        }


        public async Task<NewsDTO> CreateAsync(NewsDTO newsData)
        {
            var news = new News
            {
                Id = newsData.Id,
                Date = newsData.Date,
                Title = newsData.Title,
                Content = newsData.Content,
                AuthorId = newsData.AuthorId,
                RubricId = newsData.RubricId
            };

            await _unitOfWork.News.CreateAsync(news);

            return news?.ToDto();
        }

        public async Task<bool> UpdateAsync(NewsDTO news)
        {
            var inMemoryNews = await _unitOfWork.News.GetByIdAsync(news.Id);

            if (inMemoryNews == null)
            {
                return false;
            }

            inMemoryNews.Update(news);

            return await _unitOfWork.News.UpdateAsync(inMemoryNews);
        }

        public async Task<bool> DeleteByIdAsync(int newsId)
        {
            var news = await _unitOfWork.News.GetByIdAsync(newsId);

            if (news == null)
            {
                return false;
            }

            await _unitOfWork.News.DeleteByIdAsync(newsId);
            return true;
        }
    }
}
