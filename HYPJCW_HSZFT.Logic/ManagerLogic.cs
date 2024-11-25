using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        IRepository<Managers> managerRepo;

        public ManagerLogic(IRepository<Managers> repo)
        {
            managerRepo = repo;
        }

        public void Create(Managers manager)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Managers> GetAllManagersWithDoctorateWithouthMba()
        {
            var everyManager = managerRepo.ReadAll();

            var doctorateManagers = everyManager
                .Where(m => m.Name.ToLower().Contains("dr.%%") && !m.HasMba);

            if(doctorateManagers is null)
            {
                throw new Exception("There are no managers without MBA");
            }
            return doctorateManagers;
        }

        public Managers GetLongestWorkingManager()
        {
            throw new NotImplementedException();
        }

        public Managers GetLongestWorkingManagerComparedToHisAge()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Managers> GetManagersWithDoctorate()
        {
            throw new NotImplementedException();
        }

        public MbaRateDto GetRateOfManagersWithMbaAndWithout()
        {
            throw new NotImplementedException();
        }

        public void Read(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Managers> ReadAll()
        {
            return managerRepo.ReadAll();
        }

        public void Update(Managers manager, string id)
        {
            throw new NotImplementedException();
        }
    }
}
