using System;
using BBL.DTO;
using DAL.Models;

namespace BBL.Extensions
{
    public static class RubricExtension
    {
        public static RubricDTO ToDto(this Rubric rubric)
        {
            return new RubricDTO()
            {
                Id = rubric.Id,
                Name = rubric.Name
            };
        }

        public static void Update(this Rubric rubric, RubricDTO DTO)
        {
            rubric.Name = DTO.Name;
        }
    }
}
