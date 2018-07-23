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
        }
    }
}