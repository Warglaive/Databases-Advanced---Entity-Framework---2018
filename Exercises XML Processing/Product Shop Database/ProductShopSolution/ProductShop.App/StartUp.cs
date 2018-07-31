using System;
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
            //ADD USERS
            //    var config = new MapperConfiguration(cfg =>
            //    {
            //        cfg.AddProfile<ProductShopProfile>();
            //    });
            //    var mapper = config.CreateMapper();

            //    var xmlString = File.ReadAllText("Xml/users.xml");

            //    var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            //    var deserializeUser = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            //    var users = new List<Users>();
            //    foreach (var userDto in deserializeUser)
            //    {
            //        if (!IsValid(userDto))
            //        {
            //            continue;
            //        }
            //        var user = mapper.Map<Users>(userDto);
            //        users.Add(user);
            //    }

            //    var context = new ProductShopDbContext();
            //    context.AddRange(users);
            //    context.SaveChanges();

            //ADD Products
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ProductShopProfile>();
            //});
            //var mapper = config.CreateMapper();

            //var xmlString = File.ReadAllText("Xml/products.xml");

            //var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            //var deserializeUser = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            //var products = new List<Products>();
            //int counter = 1;
            //foreach (var productDto in deserializeUser)
            //{
            //    if (!IsValid(productDto))
            //    {
            //        continue;
            //    }

            //    var product = mapper.Map<Products>(productDto);

            //    var buyerId = new Random().Next(1, 30);
            //    var sellerId = new Random().Next(31, 56);

            //    product.BuyerId = buyerId;
            //    product.SellerId = sellerId;

            //    if (counter == 4)
            //    {
            //        product.BuyerId = null;
            //        counter = 0;
            //    }
            //    products.Add(product);
            //    counter++;
            //}

            //var context = new ProductShopDbContext();
            //context.AddRange(products);
            //context.SaveChanges();

            //ADD CATEGORIES
            var config = new MapperConfiguration(x =>
             {
                 x.AddProfile<ProductShopProfile>();
             });
            //var mapper = config.CreateMapper();
            //var xmlString = File.ReadAllText("Xml/categories.xml");
            //var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            //var deserializedCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            //var categories = new List<Categories>();
            //foreach (var categoryDto in deserializedCategories)
            //{
            //    if (!IsValid(categoryDto))
            //    {
            //        continue;
            //    }

            //    var category = mapper.Map<Categories>(categoryDto);
            //    categories.Add(category);
            //}
            //Randomly generate categories for each product from the existing categories.
            var categoryProducts = new List<CategoryProducts>();
            for (int productId = 201; productId <= 400; productId++)
            {
                var categoryId = new Random().Next(1, 12);
                var categoryProduct = new CategoryProducts()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };
                categoryProducts.Add(categoryProduct);
            }
            var context = new ProductShopDbContext();
            context.CategoryProducts.AddRange(categoryProducts);
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