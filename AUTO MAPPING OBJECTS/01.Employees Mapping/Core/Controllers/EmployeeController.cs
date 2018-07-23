using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Banicharnica.App.Core.Contracts;
using Banicharnica.App.Core.DTOs;
using Banicharnica.Data;
using Banicharnica.Models;

namespace Banicharnica.App.Core.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        private const string InvalidIdMessage = "Invalid Id";

        private readonly BanicharnicaContext context;
        private readonly IMapper mapper;
        public EmployeeController(BanicharnicaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<Employee>(employeeDto);
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = this.context.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.Address = address;
                this.context.SaveChanges();
            }
            throw new ArgumentException(InvalidIdMessage);
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = this.context.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.Birthday = date;
                this.context.SaveChanges();
            }
            throw new ArgumentException(InvalidIdMessage);
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            if (employee != null)
            {
                return employeeDto;
            }
            throw new ArgumentException(InvalidIdMessage);
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeePersonalInfoDto>(employee);

            if (employee != null)
            {
                return employeeDto;
            }
            throw new ArgumentException(InvalidIdMessage);
        }

        public List<EmployeeDto> GetEmployeesOlderThan(int age)
        {
            var employees = this.context.Employees.Where(a => DateTime.Now.Year - a.Birthday.Value.Year > age)
                .ToList();

            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                var employeeDto = Mapper.Map<EmployeeDto>(employee);
                employeeDtos.Add(employeeDto);
            }

            return employeeDtos;
        }
    }
}