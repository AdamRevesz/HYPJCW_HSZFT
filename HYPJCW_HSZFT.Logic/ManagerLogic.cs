﻿using HYPJCW_HSZFT.Data;
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
            var everyManager = managerRepo.ReadAll();
            if (manager is null)
            {
                throw new ArgumentException("Manager is null");
            }

            var lastId = everyManager
                .OrderByDescending(m => m.ManagerId)
                .Select(m => m.ManagerId)
                .FirstOrDefault();

            if (lastId != null && lastId.StartsWith("MGR"))
            {
                var numericPart = int.Parse(lastId.Substring(3));
                var newId = $"MGR{numericPart + 1:D3}";
                manager.ManagerId = newId;
            }
            else
            {
                manager.ManagerId = "MGR001";
            }

            managerRepo.Create(manager);
        }

        public void Delete(string id)
        {
            managerRepo.Delete(id);
        }

        public List<Managers> GetAllManagersWithDoctorateWithouthMba()
        {
            var everyManager = managerRepo.ReadAll();

            var doctorateManagers = everyManager
                .Where(m => m.Name.ToLower().Contains("dr.%%") && !m.HasMba).ToList();

            if (doctorateManagers is null)
            {
                throw new Exception("There are no managers without MBA");
            }
            return doctorateManagers;
        }

        public Managers GetLongestWorkingManager()
        {
            var everyManager = managerRepo.ReadAll();

            var longestWorkingManager = everyManager
                .OrderByDescending(m =>
                ((DateTime.Now - DateTime.Parse(m.StartOfEmployment)).TotalDays / 365.25) /
                ((DateTime.Now - new DateTime(m.BirthYear, 1, 1)).TotalDays / 365.25)
                )
                .FirstOrDefault();

            if (longestWorkingManager is null)
            {
                throw new Exception();
            }
            return longestWorkingManager;

        }

        public Managers GetLongestWorkingManagerComparedToHisAge()
        {
            var everyManager = managerRepo.ReadAll();

            var managerForAge = everyManager
                .Select(m => new
                {
                    Manager = m,
                    Ratio = ((DateTime.Now - DateTime.Parse(m.StartOfEmployment)).TotalDays / 365.25) /
                ((DateTime.Now - new DateTime(m.BirthYear, 1, 1)).TotalDays / 365.25)
                })
                .OrderByDescending(x => x.Ratio)
                .FirstOrDefault()?.Manager;

            if (managerForAge is null)
            {
                throw new ArgumentException();
            }
            return managerForAge;

        }

        public List<Managers> GetManagersWithDoctorate()
        {
            var everyManager = managerRepo.ReadAll();
            return everyManager
                .Where(m => m.Name.ToLower().Contains("dr.")).ToList();
        }

        public MbaRateDto GetRateOfManagersWithMbaAndWithout()
        {
            MbaRateDto rate = new MbaRateDto();
            var managers = managerRepo.ReadAll();

            var grouped = managers
                .GroupBy(m => m.HasMba)
                .Select(g => new
                {
                    HasMba = g.Key,
                    Count = g.Count()
                })
                .ToList();
            return rate;
        }

        public Managers Read(string id)
        {
            var manager = managerRepo.Read(id);
            if (manager is null)
            {
                throw new ArgumentException("Manager does not exist");
            }
            return manager;
        }

        public List<Managers> ReadAll()
        {
            return managerRepo.ReadAll().ToList();
        }

        public void Update(Managers manager, string id)
        {
            manager.ManagerId = id;
            managerRepo.Update(manager);
        }
    }
}
