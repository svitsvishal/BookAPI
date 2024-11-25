using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepository _repository;
        public BooksController(IBookRepository repository ,ILogger<BooksController> logger) 
        {
            _repository = repository;
            _logger= logger;
        }

        //api/books?filterOn=BookName&filterquery=book1
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn , [FromQuery]  string? filterQuery
           ,[FromQuery] string? sortBy , [FromQuery] bool asc=true
            , [FromQuery] int pageNumber =1, [FromQuery] int pageSize =10000)
        {
            _logger.LogInformation("calling Get all");
            var books = await _repository.GetAllAsync(filterOn,filterQuery,sortBy,asc, pageNumber , pageSize);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            await _repository.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            await _repository.UpdateAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
