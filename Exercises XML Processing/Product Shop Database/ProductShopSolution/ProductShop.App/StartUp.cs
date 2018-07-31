using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;
using ProductShop.App.Dto;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var xmlString = File.ReadAllText("Xml/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            var deserializeUser = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            var users = new List<Users>();
            foreach (var userDto in deserializeUser)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }
                var user = mapper.Map<Users>(userDto);
                users.Add(user);
            }

            var context = new ProductShopDbContext();
            context.AddRange(users);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}