using System;
using System.Collections.Generic;
using Banicharnica.App.Core.DTOs;

namespace Banicharnica.App.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);
        void SetBirthday(int employeeId, DateTime date);
        void SetAddress(int employeeId, string address);
        List<EmployeeDto> GetEmployeesOlderThan(int age);
        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);
    }
}