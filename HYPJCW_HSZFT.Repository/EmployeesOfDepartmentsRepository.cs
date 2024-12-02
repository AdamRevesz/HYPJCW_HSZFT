using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Repository
{
    public class EmployeesOfDepartmentsRepository : Repository<EmployeesOfDepartments>, IRepository<EmployeesOfDepartments>
    {
        public EmployeesOfDepartmentsRepository(MainDbContext ctx) : base(ctx)
        {

        }

        public override EmployeesOfDepartments Read(string id)
        {
            return ctx.EmployeesOfDepartments.FirstOrDefault(e => e.EmployeesOfDepartmentsId == id);
        }

        public override void Update(EmployeesOfDepartments item)
        {
            var old = Read(item.EmployeesOfDepartmentsId);
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
