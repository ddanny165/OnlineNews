using System;
using BBL.DTO;
using DAL.Models;

namespace BBL.Extensions
{
    public static class NewsExtension
    {
        public static NewsDTO ToDto(this News news)
        {
            return new NewsDTO()
            {
                Id = news.Id,
                Date = news.Date,
                Title = news.Title,
                Content = news.Content,
                AuthorId = news.AuthorId,
                RubricId = news.RubricId
            };
        }

        public static void Update(this News news, NewsDTO DTO)
        {
            news.Date = DTO.Date;
            news.Title = DTO.Title;
            news.Content = DTO.Content;
            news.AuthorId = DTO.AuthorId;
            news.RubricId = DTO.RubricId;
        }
    }
}
