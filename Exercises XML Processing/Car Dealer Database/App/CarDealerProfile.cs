using App.Dtos;
using AutoMapper;
using Models;

namespace App
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, PartCar>();
        }
    }
}