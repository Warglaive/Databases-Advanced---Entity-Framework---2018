using AutoMapper;
using Banicharnica.App.Core.DTOs;
using Banicharnica.Models;

namespace Banicharnica.App.Core
{
    public class BanicharnicaProfile : Profile
    {
        public BanicharnicaProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>()
                .ForMember(x => x.EmployeesDto, from => from.MapFrom(x => x.ManagerEmployees))
                .ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
        }
    }
}