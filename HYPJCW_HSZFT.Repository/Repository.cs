using HYPJCW_HSZFT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MainDbContext ctx;

        public Repository(MainDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(string id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }
        public List<T> ReadAll()
        {
            return ctx.Set<T>().ToList();
        }

        public abstract T Read(string id);
        public abstract void Update(T item);


    }
}
