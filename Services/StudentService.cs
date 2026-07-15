using StudentApi.Dtos;
using StudentApi.Models;
using StudentApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        private static StudentDto ToDto(Student student) => new()
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email
        };

        private static void ApplyDto(StudentDto dto, Student student)
        {
            student.Name = dto.Name;
            student.Age = dto.Age;
            student.Email = dto.Email;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            return s == null ? null : ToDto(s);
        }

        public async Task<StudentDto> CreateAsync(StudentDto dto)
        {
            var model = new Student();
            ApplyDto(dto, model);
            var created = await _repo.AddAsync(model);
            return ToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, StudentDto dto)
        {
            var exist = await _repo.GetByIdAsync(id);
            if (exist == null) return false;
            ApplyDto(dto, exist);
            await _repo.UpdateAsync(exist);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _repo.GetByIdAsync(id);
            if (exist == null) return false;
            await _repo.DeleteAsync(exist);
            return true;
        }
    }
}
