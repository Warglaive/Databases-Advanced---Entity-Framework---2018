using AutoMapper;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;

namespace FastFood.App
{
    public class FastFoodProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public FastFoodProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(n => n.Position,
                    opt => opt.Ignore());

            CreateMap<ItemDto, Item>()
                .ForMember(x => x.Category,
                    opt => opt.Ignore());
        }
    }
}