using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
            => Ok(await _context.Students.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var s = await _context.Students.FindAsync(id);
            if (s == null) return NotFound();
            return Ok(s);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (id != student.Id) return BadRequest();
            var existing = await _context.Students.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.Email = student.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Students.FindAsync(id);
            if (existing == null) return NotFound();
            _context.Students.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

