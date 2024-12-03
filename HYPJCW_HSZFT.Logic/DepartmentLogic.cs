using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic
{
    public class DepartmentLogic : IDepartmentLogic
    {
        IRepository<Departments> departmentRepo;

        public DepartmentLogic(IRepository<Departments> departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        public void Create(Departments item)
        {
            var everyDepartment = departmentRepo.ReadAll();
            if (item is null)
            {
                throw new ArgumentException("Employee is null");
            }
            var existingDepartment = everyDepartment
                .Where(d => d.Name == item.Name);
            if(item.Name == Convert.ToString(existingDepartment))
            {
                throw new ArgumentException("THis department already exists");
            }

            departmentRepo.Create(item);
        }

        public void Delete(string id)
        {
            departmentRepo.Delete(id);
        }

        public Departments Read(string id)
        {
            return departmentRepo.Read(id);
        }

        public List<Departments> ReadAll()
        {
            return departmentRepo.ReadAll();
        }

        public void Update(Departments item, string id)
        {
            item.DepartmentCode = id;
            departmentRepo.Update(item);
        }
    }
}
