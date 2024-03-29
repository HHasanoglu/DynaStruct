﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class FrameNode : ANode
    {
        #region Ctor

        public FrameNode(int NodeID, double xcoord, double ycoord) : base(NodeID, xcoord, ycoord)
        {
            _dofPerNode = 3;
        }

        #endregion

        #region Private Fields
        private eRestraint _rotationRestraint;
        private double _rotation;

        public eRestraint RotationRestraint { get => _rotationRestraint; set => _rotationRestraint = value; }
        public double Rotation { get => _rotation; set => _rotation = value; }

        public double[] DisplacementVector => new double[] { Dispx, Dispy,Rotation };
        #endregion

        #region Public Properties

        #endregion


    }
}
