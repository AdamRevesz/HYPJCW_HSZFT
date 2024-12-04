using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HYPJCW_HSZFT.Repository
{


    public class EmployeeRepository : Repository<Employees>, IRepository<Employees>
    {
        public EmployeeRepository(MainDbContext ctx) : base(ctx)
        {
        }

        public async Task Create(EmployeeDto employeeDto)
        {
            var employee = new Employees(employeeDto);

            var departmentCodes = employeeDto.Departments.Select(d => d.DepartmentCode).ToList();

            var existingDepartments = ctx.Departments
                .Where(d => departmentCodes.Contains(d.DepartmentCode))
                .ToList();

            employee.Departments = existingDepartments;

            ctx.Employees.Add(employee);

            await ctx.SaveChangesAsync();
        }

        public override Employees Read(string id)
        {
            return ctx.Employees.FirstOrDefault(x => x.EmployeeId == id) ?? throw new InvalidOperationException("Employee not found");
        }

        public new List<Employees> ReadAll()
        {
            return ctx.Employees.Include(e => e.Departments.OrderBy(d => d.Name)).ToList();
        }

        public override void Update(Employees item)
        {
            var old = Read(item.EmployeeId);
            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch (Exception)
                {
                }
            }
            ctx.SaveChanges();
        }
    }
}


