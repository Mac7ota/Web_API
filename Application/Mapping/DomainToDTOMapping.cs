using AutoMapper;
using WebAPI.Domain.DTOs;
using WebAPI.Models;

namespace WebApi.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping() 
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.NameEmployee, m => m.MapFrom(orig => orig.Name));
        }
    }
}