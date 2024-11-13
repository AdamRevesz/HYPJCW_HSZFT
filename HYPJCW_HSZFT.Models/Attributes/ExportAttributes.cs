using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Entities.Dependencies
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ToExportAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class HideFromExportAttribute : Attribute
    {

    }
}
