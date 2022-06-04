using System;
using BBL.DTO;
using DAL.Models;

namespace BBL.Extensions
{
    public static class NewsTagExtension
    {
        public static NewsTagDTO ToDto(this NewsTag newsTags)
        {
            return new NewsTagDTO()
            {
                Id = newsTags.Id,
                NewsId = newsTags.NewsId,
                TagId = newsTags.TagId
            };
        }

        public static void Update(this NewsTag newsTags, NewsTagDTO DTO)
        {
            newsTags.NewsId = DTO.NewsId;
            newsTags.TagId = DTO.TagId;
        }
    }
}
