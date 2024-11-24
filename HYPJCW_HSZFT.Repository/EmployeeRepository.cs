using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;

namespace HYPJCW_HSZFT.Repository
{
    public class EmployeeRepository : Repository<Employees> ,IRepository<Employees>
    {
        public EmployeeRepository(MainDbContext ctx) : base(ctx)
        {
        }

        public override Employees Read(string id)
        {
            return ctx.Employees.FirstOrDefault(x => x.EmployeeId == id);
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
