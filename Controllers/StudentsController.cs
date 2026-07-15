using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Dtos;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _svc;
        private readonly IValidator<StudentDto> _validator;

        public StudentsController(IStudentService svc, IValidator<StudentDto> validator)
        {
            _svc = svc;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id}", Name = "GetStudentById")]
        public async Task<IActionResult> GetById(int id)
        {
            var s = await _svc.GetByIdAsync(id);
            return s == null ? NotFound() : Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid) return ValidationProblem(validation.ToDictionary());
            var created = await _svc.CreateAsync(dto);
            return CreatedAtRoute("GetStudentById", new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDto dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid) return ValidationProblem(validation.ToDictionary());
            var ok = await _svc.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
