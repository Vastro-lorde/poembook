using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using poembook.DTOs;
using poembook.Models;
using poembook.Services;
using poembook.Services.Pagination;

namespace poembook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoemController : ControllerBase
    {
        private readonly IPoemService _poemService;
        private readonly ILogger<PoemController> _logger;
        public PoemController( ILogger<PoemController> logger, IPoemService poemService)
        {
            _logger = logger;
            _poemService = poemService;
        }

        [HttpGet]
        [Route("get-all-poems")]
        public async Task<IActionResult> GetPoems([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var result = await _poemService.GetAllPoems(paginationParams.PageNumber, paginationParams.PageSize);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*[HttpGet]
        [Route("get-random-poem")]
        public async Task<IActionResult> GetRandomPoem()
        {
            try
            {
                var result = await _poemService.GetRandomPoem();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        [HttpGet]
        [Route("get-poem-by-id/{id}")]
        public async Task<IActionResult> GetPoemById(string id)
        {
            try
            {
                var result = await _poemService.GetPoemById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("add-poem")]
        public async Task<IActionResult> CreatePoem([FromBody] CreatePoemDTO createPoem)
        {
            try
            {
                if (createPoem == null)
                {
                    return BadRequest();
                }

                var poem = new PoemModel
                {
                    Title = createPoem.Title,
                    Content = createPoem.Content,
                    Author = createPoem.Author,
                    Date = DateTime.Now
                };
                var result = await _poemService.CreatePoem(poem);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete-poem/{id}")]
        public async Task<IActionResult> DeletePoem(string id)
        {
            try
            {
                var result = await _poemService.DeletePoem(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("update-poem/{id}")]
        public async Task<IActionResult> UpdatePoem(string id, [FromBody] UpdatePoemDTO updatePoem)
        {
            try
            {
                if (updatePoem == null)
                {
                    return BadRequest();
                }

                var poem = new PoemModel
                {
                    Title = updatePoem.Title,
                    Content = updatePoem.Content,
                    Author = updatePoem.Author,
                    UpdatedAt = DateTime.Now
                };
                var result = await _poemService.EditPoem(id, poem);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
