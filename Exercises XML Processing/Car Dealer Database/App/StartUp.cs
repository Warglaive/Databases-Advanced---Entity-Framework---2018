using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using App.Dtos;
using AutoMapper;
using Data;
using Models;

namespace App
{
    public class StartUp
    {
        public static void Main()
        {
            //ImportSuppliers();
            //ImportParts();
            ImportCars();

        }

        private static void ImportCars()
        {
            var context = new CarDealerDbContext();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            var mapper = mapperConfig.CreateMapper();
            var xmlString = File.ReadAllText("ImportXmls/cars.xml");
            var serializer = new XmlSerializer(typeof(CarDto[]),
                new XmlRootAttribute("cars"));

            var deserializer = (CarDto[])serializer.Deserialize(new StringReader(xmlString));

            var cars = new List<Car>();
            foreach (var carDto in deserializer)
            {
                if (!IsValid(carDto))
                {
                    continue;
                }

                var car = mapper.Map<Car>(carDto);
                //to do add random parts to car
                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts()
        {
            var context = new CarDealerDbContext();
            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); });
            var mapper = mapperConfig.CreateMapper();
            var xmlString = File.ReadAllText("ImportXmls/parts.xml");
            var serializer = new XmlSerializer(typeof(PartDto[]),
                new XmlRootAttribute("parts"));

            var deserializeParts = (PartDto[])serializer.Deserialize(new StringReader(xmlString));


            var parts = new List<Part>();

            foreach (var deserializePart in deserializeParts)
            {
                if (!IsValid(deserializePart))
                {
                    continue;
                }
                //take random supplier
                var suppliers = context.Suppliers.ToList();
                var supplierId = new Random().Next(suppliers.Count - 1);

                var supplier = suppliers.FirstOrDefault(id => id.Id == supplierId);

                suppliers.Remove(supplier);

                var partDto = new PartDto()
                {
                    Name = deserializePart.Name,
                    Price = deserializePart.Price,
                    Quantity = deserializePart.Quantity,
                    SupplierId = supplierId
                };
                var part = mapper.Map<Part>(partDto);
                parts.Add(part);
            }
            context.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers()
        {
            var context = new CarDealerDbContext();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            var mapper = mapperConfig.CreateMapper();

            var xmlString = File.ReadAllText("ImportXmls/suppliers.xml");
            var serializer = new XmlSerializer(typeof(SupplierDto[]),
                new XmlRootAttribute("suppliers"));

            var deserializeSuppliers = (SupplierDto[])serializer.Deserialize(
                new StringReader(xmlString));

            var suppliers = new List<Supplier>();
            foreach (var deserializeSupplier in deserializeSuppliers)
            {
                if (!IsValid(deserializeSupplier))
                {
                    continue;
                }

                var supplier = mapper.Map<Supplier>(deserializeSupplier);
                suppliers.Add(supplier);
            }
            context.AddRange(suppliers);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}