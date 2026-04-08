using Library.RestApi.Data;
using Library.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.RestApi.Controllers
{
    /// Route: /api/books

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // In-memory storage 
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        private static int _nextId = 1;


        [HttpGet]
        public ActionResult<List<Book>> GetAll([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            if (limit <= 0)
                return BadRequest("Limit must be greater than 0");

            if (offset < 0)
                return BadRequest("Offset cannot be negative");

            var result = _context.Books
                .Skip(offset)
                .Take(limit)
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Title is required");

            if (book.PublishingYear < 1900)
                return BadRequest("Publishing year must be >= 1900");

            book.Id = _nextId++;
            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var existing = _context.Books.FirstOrDefault(b => b.Id == id);

            if (existing == null)
                return NotFound();

            existing.Title = updatedBook.Title;
            existing.AuthorId = updatedBook.AuthorId;
            existing.PublishingCompanyId = updatedBook.PublishingCompanyId;
            existing.PublishingYear = updatedBook.PublishingYear;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);

            return NoContent();
        }
    }
}