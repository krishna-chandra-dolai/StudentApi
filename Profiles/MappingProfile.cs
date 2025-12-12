using AutoMapper;
using StudentApi.Dtos;
using StudentApi.Models;

namespace StudentApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
