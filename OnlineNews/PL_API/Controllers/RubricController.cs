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
    public class RubricController : ControllerBase
    {
        private readonly IRubricService _rubricService;

        public RubricController(IRubricService rubricService)
        {
            _rubricService = rubricService;
        }

        [HttpGet("/api/rubrics/getAll")]
        public async Task<IActionResult> GetAll()
        {
            var rubrics = await _rubricService.GetAllAsync();

            if (!rubrics.Any())
            {
                var Error = new ApiError("Not found any rubrics.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(rubrics);
        }

        [HttpGet("/api/rubrics/getById/{rubricId}")]
        public async Task<IActionResult> GetById([FromRoute] int rubricId)
        {
            var rubric = await _rubricService.GetByIdAsync(rubricId);

            if (rubric == null)
            {
                var Error = new ApiError("Not found a rubric with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            return Ok(rubric);
        }

        [HttpPost("/api/rubrics/create")]
        public async Task<IActionResult> Create([FromBody] RubricDTO rubricData)
        {
            var rubric = await _rubricService.GetByIdAsync(rubricData.Id);

            if (rubric != null)
            {
                var Error = new ApiError("Rubric with such id already exists.",
                    HttpStatusCode.BadRequest);

                return BadRequest(Error);
            }

            rubric = await _rubricService.CreateAsync(rubricData);

            return Ok(rubric);
        }

        [HttpDelete("/api/rubrics/delete/{rubricId}")]
        public async Task<IActionResult> Delete([FromRoute] int rubricId)
        {
            var rubric = await _rubricService.GetByIdAsync(rubricId);

            if (rubric == null)
            {
                var Error = new ApiError("Not found a rubric with a given id.",
                    HttpStatusCode.NotFound);

                return NotFound(Error);
            }

            if (!await _rubricService.DeleteByIdAsync(rubricId))
            {
                var Error = new ApiError("Some unexpected error occured.",
                    HttpStatusCode.Conflict);

                return Conflict(Error);
            }

            return Ok();
        }
    }
}
