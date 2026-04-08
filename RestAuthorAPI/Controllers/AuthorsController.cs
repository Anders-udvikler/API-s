using Microsoft.AspNetCore.Mvc;
using Library.RestApi.Models;
using Library.RestApi.Data;

namespace Library.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAll()
        {
            return Ok(_context.Authors.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Create(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name) || string.IsNullOrWhiteSpace(author.Surname))
                return BadRequest("Name and surname are required");

            _context.Authors.Add(author);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Author updated)
        {
            var existing = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (existing == null)
                return NotFound();

            existing.Name = updated.Name;
            existing.Surname = updated.Surname;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return NoContent();
        }
    }
}