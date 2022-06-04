using System;
using BBL.DTO;
using DAL.Models;

namespace BBL.Extensions
{
    public static class TagExtension
    {
        public static TagDTO ToDto(this Tag tag)
        {
            return new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static void Update(this Tag tag, TagDTO DTO)
        {
            tag.Name = DTO.Name;
        }
    }
}
