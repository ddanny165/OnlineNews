using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BBL.Services.Interfaces;
using BBL.DTO;
using PL_API.Models;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("/api/tags/getAll")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllAsync();

            if (!tags.Any())
            {
                var Error = new ApiError("Not found any tags.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(tags);
        }

        [HttpGet("/api/tags/getById/{tagId}")]
        public async Task<IActionResult> GetById([FromRoute] int tagId)
        {
            var tag = await _tagService.GetByIdAsync(tagId);

            if (tag == null)
            {
                var Error = new ApiError("Not found a tag with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(tag);
        }

        [HttpPost("/api/tags/create")]
        public async Task<IActionResult> Create([FromBody] TagDTO tagData)
        {
            var tag = await _tagService.GetByIdAsync(tagData.Id);

            if (tag != null)
            {
                var Error = new ApiError("Tag with such id already exists.",
                    HttpStatusCode.BadRequest);

                return BadRequest(Error);
            }

            tag = await _tagService.CreateAsync(tagData);

            return Ok(tag);
        }

        [HttpDelete("/api/tags/delete/{tagId}")]
        public async Task<IActionResult> Delete([FromRoute] int tagId)
        {
            var tag = await _tagService.GetByIdAsync(tagId);

            if (tag == null)
            {
                var Error = new ApiError("Not found a tag with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _tagService.DeleteByIdAsync(tagId))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok();
        }
    }
}
