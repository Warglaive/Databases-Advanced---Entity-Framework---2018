using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Banicharnica.App.Core.Contracts;
using Banicharnica.App.Core.DTOs;
using Banicharnica.Data;

namespace Banicharnica.App.Core.Controllers
{
    public class ManagerController : IManagerController
    {
        private readonly BanicharnicaContext context;
        public ManagerController(BanicharnicaContext context)
        {
            this.context = context;
        }

        public ManagerDto GetManagerInfo(int employeeId)
        {
            var employee = context.Employees
                .Where(i => i.Id == employeeId)
                .ProjectTo<ManagerDto>()
                .SingleOrDefault();
            if (employee == null)
            {
                throw new ArgumentException("invalid id");
            }

            return employee;
        }

        public void SetManagerCommand(int employeeId, int managerId)
        {
            var employee = context.Employees.Find(employeeId);

            var manager = context.Employees.Find(managerId);
            if (employee == null || manager == null)
            {
                throw new ArgumentException("invalid id");
            }

            employee.Manager = manager;
            context.SaveChanges();
        }
    }
}
