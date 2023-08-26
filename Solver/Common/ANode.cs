using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public abstract class ANode
    {
        #region Ctor
        public ANode(int NodeID, double xcoord, double ycoord)
        {
            _NodeID = NodeID;
            _xcoord = xcoord;
            _ycoord = ycoord;
        }

        #endregion

        #region Private Fields

        protected double _xcoord;
        protected double _ycoord;
        protected int _NodeID;
        protected int _dofPerNode;
        protected eRestraint _xRestraint;
        protected eRestraint _yRestraint;
        protected double _fx;
        protected double _fy;
        protected double _dispx;
        protected double _dispy;


        #endregion


        #region Public Properties
        public double Xcoord { get => _xcoord; set => _xcoord = value; }
        public double Ycoord { get => _ycoord; set => _ycoord = value; }
        public int ID { get => _NodeID; set => _NodeID = value; }
        public int DofPerNode { get => _dofPerNode; set => _dofPerNode = value; }

        public eRestraint XRestraint { get => _xRestraint; set => _xRestraint = value; }
        public eRestraint YRestraint { get => _yRestraint; set => _yRestraint = value; }
        public double Fx { get => _fx; set => _fx = value; }
        public double Fy { get => _fy; set => _fy = value; }
        public double Dispx { get => _dispx; set => _dispx = value; }
        public double Dispy { get => _dispy; set => _dispy = value; }

        public double Dispxy { get => Math.Sqrt(Math.Pow(_dispx, 2) + Math.Pow(_dispy, 2)); }

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
