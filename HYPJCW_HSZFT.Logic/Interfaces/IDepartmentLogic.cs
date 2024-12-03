using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IDepartmentLogic
    {
        void Create(Departments item);
        Departments Read(string id);
        void Update(Departments item, string id);
        List<Departments> ReadAll();
        void Delete(string id);
    }
}
