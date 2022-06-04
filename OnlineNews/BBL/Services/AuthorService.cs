using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BBL.DTO;
using BBL.Extensions;
using BBL.Interfaces;
using DAL.Repositories;
using DAL.Models;

namespace BBL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorDTO> GetByIdAsync(int authorId)
        {
            var author = await _unitOfWork
                .Author
                .GetByIdAsync(authorId);

            return author?.ToDto();
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var authors = await _unitOfWork.Author.GetAllAsync();
            var result = authors.Select(aut => aut?.ToDto());

            return result;
        }

        public async Task<AuthorDTO> CreateAsync(AuthorDTO authorData)
        {
            var author = new Author
            {
                Id = authorData.Id,
                Username = authorData.Username,
                Email = authorData.Email,
                Password = authorData.Password
            };

            await _unitOfWork.Author.CreateAsync(author);

            return author?.ToDto();
        }

        public async Task<bool> UpdateAsync(AuthorDTO author)
        {
            var inMemoryAuthor = await _unitOfWork.Author.GetByIdAsync(author.Id);

            if (inMemoryAuthor == null)
            {
                return false;
            }

            inMemoryAuthor.Update(author);

            return await _unitOfWork.Author.UpdateAsync(inMemoryAuthor);
        }

        public async Task<bool> DeleteByIdAsync(int authorId)
        {
            var author = await _unitOfWork.Author.GetByIdAsync(authorId);

            if (author == null)
            {
                return false;
            }

            await _unitOfWork.Author.DeleteByIdAsync(authorId);
            return true;
        }
    }
}
