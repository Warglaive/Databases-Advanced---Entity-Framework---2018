using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                IsEmployeeUnique(employeeDto, deserializedEmployees);
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var employee = Mapper.Map<Employee>(employeeDto);
                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().Trim();
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
            throw new NotImplementedException();
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