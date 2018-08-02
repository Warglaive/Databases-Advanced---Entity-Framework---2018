using App.Dtos;
using AutoMapper;
using Models;

namespace App
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersDto, Supplier>();
        }
    }
}