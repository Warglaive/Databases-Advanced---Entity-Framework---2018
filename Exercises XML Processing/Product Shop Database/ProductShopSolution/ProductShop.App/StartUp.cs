using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.App.Dto.Export;
using ProductShop.App.Dto.Import;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main()
        {
            //ReadProductsXml();
            //ReadCategoriesXml();
            //ProductsInRange();
            //SoldProducts();
            //CategoriesByProductCount();
            var context = new ProductShopDbContext();

            var users = new LastTaskExportUsersDto
            {
                Count = context.Users.Count(),
                Users = context.Users
                    .Where(x => x.ProductsSold.Any())
                    .Select(x => new LastTaskUserDto
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Age = x.Age.ToString(),
                        SoldProducts = new LastTaskSoldProduct
                        {
                            Count = x.ProductsSold.Count,
                            Products = x.ProductsSold.Select(k => new LastTaskProduct
                            {
                                Name = k.Name,
                                Price = k.Price
                            }).ToArray()
                        }
                    }).ToArray()
            };

            var sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(LastTaskExportUsersDto), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);
            File.WriteAllText("ExportedXmls/users-and-products.xml", sb.ToString());
        }

        private static void ReadCategoriesXml()
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();
            var xmlString = File.ReadAllText("Xml/categories.xml");
            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializedCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            var categories = new List<Categories>();
            foreach (var categoryDto in deserializedCategories)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                var category = mapper.Map<Categories>(categoryDto);
                categories.Add(category);
            }
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

        private static void ReadProductsXml()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var xmlString = File.ReadAllText("Xml/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            var deserializeUser = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            var products = new List<Products>();
            int counter = 1;
            foreach (var productDto in deserializeUser)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                var product = mapper.Map<Products>(productDto);

                var buyerId = new Random().Next(1, 30);
                var sellerId = new Random().Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }
                products.Add(product);
                counter++;
            }

            var context = new ProductShopDbContext();
            context.AddRange(products);
            context.SaveChanges();
        }

        private static void SoldProducts()
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

        private static void ProductsInRange()
        {
            var context = new ProductShopDbContext();
            var products = context.Products.Where(x => x.Price >= 1000 && x.Price <= 2000
                                                                       && x.Buyer != null)
                .OrderByDescending(x => x.Price)
                .Select(x => new ExportProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = $"{x.Buyer.FirstName} {x.Buyer.LastName}"
                })
                .ToArray();

            var xmlNamespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("products"));

            var path = new StringBuilder();

            serializer.Serialize(new StringWriter(path), products, xmlNamespace);

            File.WriteAllText("ExportedXmls/products-in-range.xml", path.ToString());
        }

        private static void CategoriesByProductCount()
        {
            var context = new ProductShopDbContext();

            var categories = context.Categories
                .OrderByDescending(b => b.CategoryProducts.Count)
                .Select(x => new ExportCategoryDto
                {
                    Name = x.Name,
                    ProductsCount = x.CategoryProducts.Count,
                    AveragePrice = x.CategoryProducts.Select(c => c.Products.Price)
                        .DefaultIfEmpty(0).Average(),
                    TotalRevenue = x.CategoryProducts.Select(c => c.Products.Price)
                        .DefaultIfEmpty(0).Sum()
                }).ToArray()
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportCategoryDto[]), new XmlRootAttribute("categories"));
            var xmlNamespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });


            serializer.Serialize(new StringWriter(sb), categories, xmlNamespace);
            File.WriteAllText("ExportedXmls/categories-by-products.xml", sb.ToString());
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}