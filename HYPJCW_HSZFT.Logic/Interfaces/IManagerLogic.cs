using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IManagerLogic
    {
        void Create(Managers manager);
        Managers Read(string id);
        void Update(Managers manager,string id);
        void Delete(string id);
        IQueryable<Managers> ReadAll();
        IQueryable<Managers> GetManagersWithDoctorate();
        IQueryable<Managers> GetAllManagersWithDoctorateWithouthMba();
        Managers GetLongestWorkingManager();
        Managers GetLongestWorkingManagerComparedToHisAge();
        void GetRateOfManagersWithMbaAndWithout();
        
    }
}
