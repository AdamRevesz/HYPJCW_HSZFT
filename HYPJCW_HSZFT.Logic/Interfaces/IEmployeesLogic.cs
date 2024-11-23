using HYPJCW_HSZFT.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IEmployeesLogic
    {
        void Create(Employees item) { }
        void Read(string id) { }
        void Update(Employees item, string id) { }
        IQueryable<Employees> ReadAll();
        void Delete(string id) { }
    }
}
