using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Repository
{
    public class ManagersRepository : Repository<Managers>, IRepository<Managers>
    {
        public ManagersRepository(MainDbContext ctx) : base(ctx)
        {

        }
        public override Managers Read(string id)
        {
            return ctx.Managers.FirstOrDefault(x => x.ManagerId == id);
        }

        public override void Update(Managers item)
        {
            var old = Read(item.ManagerId);
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
