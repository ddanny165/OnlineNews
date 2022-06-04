using System;
using BBL.DTO;
using DAL.Models;

namespace BBL.Extensions
{
    public static class AuthorExtension
    {
        public static AuthorDTO ToDto(this Author author)
        {
            return new AuthorDTO()
            {
                Id = author.Id,
                Username = author.Username,
                Email = author.Email,
                Password = author.Password
            };
        }

        public static void Update(this Author author, AuthorDTO DTO)
        {
            author.Username = DTO.Username;
            author.Email = DTO.Email;
            author.Password = DTO.Password;
        }
    }
}
