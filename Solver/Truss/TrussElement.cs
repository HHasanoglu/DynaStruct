using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class TrussElement : IElement
    {
        #region Ctor

        public TrussElement(int memberLabel, ANode NodeI, ANode NodeJ, double E, double A)
        {
            _Id = memberLabel;
            _nodes = new List<ANode>() { NodeI, NodeJ };
            _E = E;
            _A = A;
        }

        #endregion

        #region Private Fields

        private int _Id;
        private double _E;
        private double _A;
        private double _I;
        private List<ANode> _nodes;

        #endregion

        #region Public Properties

        public int Id =>_Id;
        public List<ANode> Nodes => _nodes;
        public TrussNode NodeI  => (TrussNode)_nodes[0];
        public TrussNode NodeJ => (TrussNode)_nodes[1];
        public double A => _A;
        public double E => _E;
        public double L => GetMemberLength();
        public double Angle => getMemberAngleAsDegree();
        public double IEndForce => 0;
        public double JEndForce => 0;



        #endregion

        #region Private Methods

        private double GetMemberLength()
        {
            var NodeI = _nodes[0];
            var NodeJ = _nodes[1];
            return Math.Sqrt(Math.Pow(NodeJ.Xcoord - NodeI.Xcoord, 2) + Math.Pow(NodeJ.Ycoord - NodeI.Ycoord, 2));
        }

        private double getMemberAngle()
        {
            var NodeI = _nodes[0];
            var NodeJ = _nodes[1];
            return Math.Atan2((NodeJ.Ycoord - NodeI.Ycoord), (NodeJ.Xcoord - NodeI.Xcoord));
        }

        private double getMemberAngleAsDegree()
        {
            return getMemberAngle() * 180 / Math.PI;
        }

        #endregion

        #region Public Methods
        public Matrix<double> GetTransposeMatrix()
        {
            var theta = getMemberAngle();
            var transposeMatrix = Matrix<double>.Build.Dense(2, 4);
            var cosTeta= Math.Abs(Math.Cos(theta)) < 10e-10 ? 0 : Math.Cos(theta);
            var sinTeta= Math.Abs(Math.Sign(theta)) < 10e-10 ? 0 : Math.Sin(theta);

            transposeMatrix[0, 0] = cosTeta;
            transposeMatrix[0, 1] = sinTeta;

            transposeMatrix[1, 2] = cosTeta;
            transposeMatrix[1, 3] = sinTeta;

            //transposeMatrix[2, 2] = cosTeta;
            //transposeMatrix[2, 3] = sinTeta;

            //transposeMatrix[3, 2] = -sinTeta;
            //transposeMatrix[3, 3] = cosTeta;

            return transposeMatrix;
        }

        public Matrix<double> GetLocalStiffnessMatrix()
        {
            /*
            [    EA/L  ,     -EA/L   ]      
            [   -EA/L    ,    EA/L   ]       
            */

            var L = GetMemberLength();

            var K = _E * _A / L;

            var kLocal = Matrix<double>.Build.Dense(2, 2);
            kLocal[0, 0] = K;
            kLocal[0, 1] = -K;
            kLocal[1, 0] = -K;
            kLocal[1, 1] = K;

            return kLocal;
        }

        public Matrix<double> GetGlobalStiffnessMatrix()
        {
            var T = GetTransposeMatrix();
            var kLocal = GetLocalStiffnessMatrix();
            return (T.Transpose() * kLocal) * T;
        }

        public Matrix<double> GetGlobalDisplacementVector()
        {
            var NumberOfNode = Nodes.Count;
            var dofPerNode = Nodes[0].DofPerNode;

            var Displacement = Matrix<double>.Build.Dense(dofPerNode * NumberOfNode, 1);

            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes[i].DofPerNode; j++)
                {
                    var node = (FrameNode)Nodes[i];
                    Displacement[dofPerNode * i + j, 0] = node.DisplacementVector[j];
                }
            }

            return Displacement;
        }

        public Matrix<double> GetLocalDisPlacementVector()
        {
            return GetTransposeMatrix() * GetGlobalDisplacementVector();
        }

        public Matrix<double> GetLocalForceVector()
        {
            return GetLocalStiffnessMatrix() * GetLocalDisPlacementVector();
        }

        #endregion
    }
}
