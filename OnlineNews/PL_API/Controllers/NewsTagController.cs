using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BBL.Services.Interfaces;
using BBL.Interfaces;
using BBL.DTO;
using PL_API.Models;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsTagController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ITagService _tagService;
        private readonly INewsTagService _newsTagService;

        public NewsTagController(INewsService newsService, ITagService tagService,
            INewsTagService newsTagService)
        {
            _newsService = newsService;
            _tagService = tagService;
            _newsTagService = newsTagService;
        }

        [HttpGet("/api/newsTags/getAll")]
        public async Task<IActionResult> GetAll()
        {
            var newsTags = await _newsTagService.GetAllAsync();

            if (!newsTags.Any())
            {
                var Error = new ApiError("NewsTags were not found", HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var newsTagsFull = new List<NewsModel>();
            foreach (var newsTag in newsTags)
            {
                var news = await _newsService.GetByIdAsync(newsTag.NewsId);
                var tag = await _tagService.GetByIdAsync(newsTag.TagId);

                newsTagsFull.Add(new NewsModel()
                {
                    Id = newsTag.Id,

                    TagId = tag.Id,
                    TagName = tag.Name,

                    NewsId = news.Id,
                    Date = news.Date,
                    Title = news.Title,
                    Content = news.Content,
                    AuthorId = news.AuthorId,
                    RubricId = news.RubricId
                });
            }

            return Ok(newsTagsFull);
        }

        [HttpGet("/api/newsTags/getById/{newsTagsId}")]
        public async Task<IActionResult> GetById([FromRoute] int newsTagsId)
        {
            var newsTags = await _newsTagService.GetByIdAsync(newsTagsId);

            if (newsTags == null)
            {
                var Error = new ApiError("Not found newsTags with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var news = await _newsService.GetByIdAsync(newsTags.NewsId);
            var tag = await _tagService.GetByIdAsync(newsTags.TagId);

            var newsTagsFull = new NewsModel()
            {
                Id = newsTags.Id,

                TagId = tag.Id,
                TagName = tag.Name,

                NewsId = news.Id,
                Date = news.Date,
                Title = news.Title,
                Content = news.Content,
                AuthorId = news.AuthorId,
                RubricId = news.RubricId
            };

            return Ok(newsTagsFull);
        }

        [HttpGet("/api/newsTags/getNewsByTagId/{tagId}")]
        public async Task<IActionResult> GetNewsByTagId([FromRoute] int tagId)
        {
            var tags = await _tagService.GetAllAsync();

            if (tags == null)
            {
                var Error = new ApiError("No tags were not found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var newsTags = await _newsTagService.GetAllAsync();

            if (newsTags == null)
            {
                var Error = new ApiError("NewsTags were not found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var recordsByTag = newsTags.Where(n => n.TagId == tagId).ToList();

            if (!recordsByTag.Any())
            {
                var Error = new ApiError("No news for such a tag was found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var newsByTag = new List<NewsDTO>();
            foreach (var recordByTag in recordsByTag)
            {
                newsByTag.Add(await _newsService.GetByIdAsync(recordByTag.NewsId));
            }


            return Ok(newsByTag);
        }

        [HttpGet("/api/newsTags/getNewsByTagName/{tagName}")]
        public async Task<IActionResult> GetNewsByTagName([FromRoute] string tagName)
        {
            var tags = await _tagService.GetAllAsync();

            if (tags == null)
            {
                var Error = new ApiError("No tags were not found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var newsTags = await _newsTagService.GetAllAsync();

            if (newsTags == null)
            {
                var Error = new ApiError("NewsTags were not found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            int inputTagId = tags.First(t => t.Name == tagName).Id;

            if (inputTagId == 0)
            {
                var Error = new ApiError("No tag with such a name was found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var recordsByTag = newsTags.Where(n => n.TagId == inputTagId).ToList();

            if (!recordsByTag.Any())
            {
                var Error = new ApiError("No news for such a tag was found",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var newsByTag = new List<NewsDTO>();
            foreach (var recordByTag in recordsByTag)
            {
                newsByTag.Add(await _newsService.GetByIdAsync(recordByTag.NewsId));
            }


            return Ok(newsByTag);
        }

        [HttpPost("/api/newsTags/create")]
        public async Task<IActionResult> Create([FromBody] NewsTagDTO newsTagsData)
        {
            var newsTags = await _newsTagService.GetByIdAsync(newsTagsData.Id);

            if (newsTags != null)
            {
                var Error = new ApiError("NewsTags with such id already exist.",
                    HttpStatusCode.BadRequest);

                return NotFound(Error);
            }

            newsTags = await _newsTagService.CreateAsync(newsTagsData);

            return Ok(newsTags);
        }

        [HttpPut("/api/newsTags/update")]
        public async Task<IActionResult> Update([FromBody] NewsTagDTO newsTagsData)
        {
            var newsTags = await _newsTagService.GetByIdAsync(newsTagsData.Id);

            if (newsTags == null)
            {
                var Error = new ApiError("NewsTags with such id does not exist.",
                    HttpStatusCode.BadRequest);

                return NotFound(Error);
            }

            if (!await _newsTagService.UpdateAsync(newsTagsData))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok(newsTagsData);
        }

        [HttpDelete("/api/newsTags/delete/{newsTagsId}")]
        public async Task<IActionResult> Delete([FromRoute] int newsTagsId)
        {
            var newsTags = await _newsTagService.GetByIdAsync(newsTagsId);

            if (newsTags == null)
            {
                var Error = new ApiError("Not found NewsTags with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _newsTagService.DeleteByIdAsync(newsTagsId))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok();
        }
    }
}
