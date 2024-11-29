using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IImportLogic
    {
        public Task GetManagersJson(string url);
        public Task GetEmployeesXml(string url);
    }
}
