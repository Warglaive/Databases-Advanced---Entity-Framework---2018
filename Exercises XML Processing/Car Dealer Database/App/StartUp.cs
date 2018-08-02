using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
            var context = new CarDealerDbContext();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            var mapper = mapperConfig.CreateMapper();

            var xmlString = File.ReadAllText("ImportXmls/suppliers.xml");
            var serializer = new XmlSerializer(typeof(SuppliersDto[]),
                new XmlRootAttribute("suppliers"));

            var deserializeSuppliers = (SuppliersDto[])serializer.Deserialize(
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