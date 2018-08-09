using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);
            var employees = new List<Employee>();
            foreach (var employeeDto in deserializedEmployees)
            {
                if (!IsEmployeeUnique(employeeDto, deserializedEmployees) || !IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var employee = Mapper.Map<Employee>(employeeDto);

                var position = GetPosition(context, employeeDto.Position);

                employee.Position = position;

                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static Position GetPosition(FastFoodDbContext context, string positionName)
        {
            var position = context.Positions.FirstOrDefault(x => x.Name == positionName);
            if (position == null)
            {
                position = new Position
                {
                    Name = positionName
                };
                context.Positions.Add(position);
                context.SaveChanges();
            }

            return position;
        }

        private static bool IsEmployeeUnique(EmployeeDto employeeDto, EmployeeDto[] deserializedEmployees)
        {
            var counter = 0;
            var currentItem = employeeDto;
            for (int j = 0; j < deserializedEmployees.Length; j++)
            {
                var nextItem = deserializedEmployees[j];
                if (currentItem.Name == nextItem.Name)
                {
                    counter++;
                }
            }


            if (counter > 1)
            {
                return false;
            }

            return true;
        }


        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var items = new List<Item>();
            var sb = new StringBuilder();
            var deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);
            foreach (var itemDto in deserializedItems)
            {
                var item = Mapper.Map<Item>(itemDto);
                var category = GetCategory(context, itemDto.Category);
                item.Category = category;

                if (!IsValid(item) || !IsValid(item.Category))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                items.Add(item);
                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }
            context.Items.AddRange(items);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static Category GetCategory(FastFoodDbContext context, string itemCategoryName)
        {
            var category = context.Categories.FirstOrDefault(x => x.Name == itemCategoryName);
            if (category == null)
            {
                category = new Category
                {
                    Name = itemCategoryName
                };
                if (IsValid(category))
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
                else
                {
                    category = null;
                }
            }

            return category;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
        private static bool IsValid(object deserializedEmployee)
        {
            var validationContext = new ValidationContext(deserializedEmployee);
            var result = new List<ValidationResult>();
            return Validator.TryValidateObject(deserializedEmployee, validationContext, result, true);
        }
    }
}