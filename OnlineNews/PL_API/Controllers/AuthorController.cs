using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BBL.Interfaces;
using BBL.DTO;
using PL_API.Models;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("/api/authors/create")]
        public async Task<IActionResult> Create([FromBody] AuthorDTO authorData)
        {
            var author = await _authorService.GetByIdAsync(authorData.Id);

            if (author != null)
            {
                var Error = new ApiError("Author with such id already exists.",
                    HttpStatusCode.BadRequest);

                return BadRequest(Error);
            }

            author = await _authorService.CreateAsync(authorData);

            return Ok(author);
        }

        [Authorize]
        [HttpPut("/api/authors/update")]
        public async Task<IActionResult> Update([FromBody] AuthorDTO authorData)
        {
            var author = await _authorService.GetByIdAsync(authorData.Id);

            if (author == null)
            {
                var Error = new ApiError("Author with such id does not exist.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _authorService.UpdateAsync(authorData))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok(authorData);
        }

        [Authorize]
        [HttpDelete("/api/authors/delete/{authorId}")]
        public async Task<IActionResult> Delete([FromRoute] int authorId)
        {
            var author = await _authorService.GetByIdAsync(authorId);

            if (author == null)
            {
                var Error = new ApiError("Not found an author with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _authorService.DeleteByIdAsync(authorId))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok();
        }
    }
}
