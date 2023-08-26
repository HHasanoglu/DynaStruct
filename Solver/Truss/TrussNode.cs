﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class TrussNode : ANode
    {
        #region Ctor

        public TrussNode(int NodeID, double xcoord, double ycoord) : base(NodeID, xcoord, ycoord)
        {
            _dofPerNode = 2;
        }

        #endregion

        #region Private Fields

        #endregion

        #region Public Properties

        #endregion


    }
}
