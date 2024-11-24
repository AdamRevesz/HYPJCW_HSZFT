using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Repository
{
    public class DepartmentsRepository : Repository<Departments>, IRepository<Departments>
    {
        public DepartmentsRepository(MainDbContext ctx) :base(ctx)
        {

        }

        public override Departments Read(string id)
        {
            return ctx.Departments.FirstOrDefault(x => x.DepartmentCode == id);
        }

        public override void Update(Departments item)
        {
            var old = Read(item.DepartmentCode);
            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch(Exception)
                {

                }
            }
            ctx.SaveChanges();
        }
    }
}
