using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main()
        {
            //ImportUsers();
            //ImportProducts();
            //ImportCategories();

            var context = new ProductShopContext();

            var categoryProducts = new List<CategoryProduct>();

            for (int productId = 42; productId <= 200; productId++)
            {
                var categoryId = new Random().Next(1, 12);

                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

        }

        private static void ImportCategories()
        {
            var categoriesJsonString = File.ReadAllText("Json/categories.json");
            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(categoriesJsonString);
            var categories = new List<Category>();
            foreach (var deserializedCategory in deserializedCategories)
            {
                if (IsValid(deserializedCategory))
                {
                    categories.Add(deserializedCategory);
                }
            }
            var context = new ProductShopContext();
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProducts()
        {
            var jsonString = File.ReadAllText("Json/products.json");

            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);

            var products = new List<Product>();
            var context = new ProductShopContext();
            foreach (var product in deserializedProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }
                var sellerId = new Random().Next(1, 35);
                var buyerId = new Random().Next(35, 57);

                var random = new Random().Next(1, 4);
                product.SellerId = sellerId;
                product.BuyerId = buyerId;
                if (random == 3)
                {
                    product.BuyerId = null;
                }
                //generate categories
                products.Add(product);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var jsonString = File.ReadAllText("Json/users.json");
            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);

            var users = new List<User>();

            foreach (var deserializedUser in deserializedUsers)
            {
                if (IsValid(deserializedUser))
                {
                    users.Add(deserializedUser);
                }
            }
            var context = new ProductShopContext();
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object deserializedUser)
        {
            var validationContext = new ValidationContext(deserializedUser);
            var result = new List<ValidationResult>();
            return Validator.TryValidateObject(deserializedUser, validationContext, result, true);
        }
    }
}