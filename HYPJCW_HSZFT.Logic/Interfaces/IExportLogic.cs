using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IExportLogic
    {
        void ExportToXml(Type[] typesToExport);
    }
}
