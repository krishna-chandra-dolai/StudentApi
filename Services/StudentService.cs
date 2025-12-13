using AutoMapper;
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
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(list);
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            return s == null ? null : _mapper.Map<StudentDto>(s);
        }

        public async Task<StudentDto> CreateAsync(StudentDto dto)
        {
            var model = _mapper.Map<Student>(dto);
            var created = await _repo.AddAsync(model);
            return _mapper.Map<StudentDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, StudentDto dto)
        {
            var exist = await _repo.GetByIdAsync(id);
            if (exist == null) return false;
            _mapper.Map(dto, exist);
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
