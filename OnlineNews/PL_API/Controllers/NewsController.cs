using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BBL.Services.Interfaces;
using BBL.Interfaces;
using BBL.DTO;
using PL_API.Models;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IAuthorService _authorService;
        private readonly IRubricService _rubricService;

        public NewsController(INewsService newsService, IAuthorService authorService,
            IRubricService rubricService)
        {
            _newsService = newsService;
            _authorService = authorService;
            _rubricService = rubricService;
        }

        [HttpGet("/api/news/getById/{newsId}")]
        public async Task<IActionResult> GetById([FromRoute] int newsId)
        {
            var news = await _newsService.GetByIdAsync(newsId);

            if (news == null)
            {
                var Error = new ApiError("Not found news with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(news);
        }

        [HttpGet("/api/news/getAllByDay")]
        public async Task<ActionResult> GetAllByDay([FromRoute] int day, int month, int year)
        {
            var newsByDay = (await _newsService.GetAllAsync())
                .Where(n => n.Date.Day == day && n.Date.Month == month
                && n.Date.Year == year).ToList();

            if (!newsByDay.Any())
            {
                var Error = new ApiError("Not found news with a given date.",
                                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(newsByDay);
        }

        [HttpGet("/api/news/getAllByMonth")]
        public async Task<ActionResult> GetAllByMonth([FromRoute] int month, int year)
        {
            var newsByDay = (await _newsService.GetAllAsync())
                .Where(n => n.Date.Month == month
                && n.Date.Year == year).ToList();

            if (!newsByDay.Any())
            {
                var Error = new ApiError("Not found news with a given date.",
                                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(newsByDay);
        }


        [HttpGet("/api/news/getAllByAuthorId/{authorId}")]
        public async Task<IActionResult> GetAllByAuthorId([FromRoute] int authorId)
        {
            var author = await _authorService.GetByIdAsync(authorId);

            if (author == null)
            {
                var Error = new ApiError("Not found an author with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var news = await _newsService.GetAllAsync();
            var currentAuthorNews = news.Where(n => n.AuthorId == authorId).ToList();

            if (!currentAuthorNews.Any())
            {
                var Error = new ApiError("News with this author are not found.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(currentAuthorNews);
        }

        [HttpGet("/api/news/getAllByRubricId/{rubricId}")]
        public async Task<IActionResult> GetAllByRubricId([FromRoute] int rubricId)
        {
            var rubric = await _rubricService.GetByIdAsync(rubricId);

            if (rubric == null)
            {
                var Error = new ApiError("Not found an rubric with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            var news = await _newsService.GetAllAsync();
            var currentRubricNews = news.Where(n => n.RubricId == rubricId).ToList();

            if (!currentRubricNews.Any())
            {
                var Error = new ApiError("News with this rubric are not found.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(currentRubricNews);
        }

        [HttpGet("/api/news/getAll")]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllAsync();

            if (!news.Any())
            {
                var Error = new ApiError("Not found any news.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(news);
        }

        [Authorize]
        [HttpPost("/api/news/create")]
        public async Task<IActionResult> Create([FromBody] NewsDTO newsData)
        {
            var news = await _newsService.GetByIdAsync(newsData.Id);

            if (news != null)
            {
                var Error = new ApiError("News with such id already exists.",
                    HttpStatusCode.BadRequest);

                return BadRequest(Error);
            }

            news = await _newsService.CreateAsync(newsData);
            return Ok(news);
        }

        [Authorize]
        [HttpPut("/api/news/update")]
        public async Task<IActionResult> Update([FromBody] NewsDTO newsData)
        {
            var news = await _newsService.GetByIdAsync(newsData.Id);

            if (news == null)
            {
                var Error = new ApiError("Not found news with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _newsService.UpdateAsync(newsData))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok(newsData);
        }

        [Authorize]
        [HttpDelete("/api/news/delete/{newsId}")]
        public async Task<IActionResult> Delete([FromRoute] int newsId)
        {
            var news = await _newsService.GetByIdAsync(newsId);

            if (news == null)
            {
                var Error = new ApiError("Not found news with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _newsService.DeleteByIdAsync(newsId))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok();
        }

    }
}
