using Microsoft.AspNetCore.Mvc;
using Library.RestApi.Models;
using Library.RestApi.Data;

namespace Library.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public PublishersController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Publisher>> GetAll()
        {
            return Ok(_context.Publishers.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Publisher> GetById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        [HttpPost]
        public ActionResult<Publisher> Create(Publisher publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher.Name))
                return BadRequest("Name is required");

            _context.Publishers.Add(publisher);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Publisher updated)
        {
            var existing = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return NotFound();

            existing.Name = updated.Name;

            _context.SaveChanges();

            return NoContent();
        }
      
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (publisher == null)
                return NotFound();

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();

            return NoContent();
        }
    }
}