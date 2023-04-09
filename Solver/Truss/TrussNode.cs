using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class TrussNode : Node
    {
        #region Ctor

        public TrussNode(int NodeID, double xcoord, double ycoord) : base(NodeID, xcoord, ycoord)
        {
            _dofPerNode = 2;
        }

        #endregion

        #region Private Fields

        private eRestraint _xRestraint;
        private eRestraint _yRestraint;
        private double _fx;
        private double _fy;
        private double _dispx;
        private double _dispy;

        #endregion

        #region Public Properties

        public eRestraint XRestraint { get => _xRestraint; set => _xRestraint = value; }
        public eRestraint YRestraint { get => _yRestraint; set => _yRestraint = value; }
        public double Fx { get => _fx; set => _fx = value; }
        public double Fy { get => _fy; set => _fy = value; }
        public double Dispx { get => _dispx; set => _dispx = value; }
        public double Dispy { get => _dispy; set => _dispy = value; }

        public double getXcoordFinal(int maginifactionFactor = 100)
        {
            return _xcoord + maginifactionFactor * _dispx;
        }
        public double getYcoordFinal(int maginifactionFactor = 100)
        {
            return _ycoord + maginifactionFactor * _dispy;
        }
        #endregion


    }
}
