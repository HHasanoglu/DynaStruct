using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public enum eRestraint
    {
        Free,
        Restrained
    }

    public enum eResultToShow
    {
        Dispx,
        Dispy,
        Dispxy,
        stress,
        strain
    }
}
