using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Frame
{
    public class FrameElement : IElement
    {
        #region Ctor

        public FrameElement(int memberLabel, ANode NodeI, ANode NodeJ, double E, double A, double I)
        {

            _Id = memberLabel;
            _nodes = new List<ANode>() { NodeI, NodeJ };
            _E = E;
            _A = A;
            _I = I;
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

        public int Id { get => _Id; }
        public List<ANode> Nodes => _nodes;
        public double E => _E;
        public double A => _A;

        #endregion

        #region Interface Implementation

        public double L => GetMemberLength();

        public double MemberAngle => getMemberAngleAsDegree();

        public FrameNode NodeI => (FrameNode)_nodes[0];

        public FrameNode NodeJ => (FrameNode)_nodes[1];

        public Matrix<double> GetTransposeMatrix()
        {
            var theta = getMemberAngle();
            var transposeMatrix = Matrix<double>.Build.Dense(6, 6);

            transposeMatrix[0, 0] = Math.Abs(Math.Cos(theta)) < 10e-10 ? 0 : Math.Cos(theta);
            transposeMatrix[0, 1] = Math.Abs(Math.Sign(theta)) < 10e-10 ? 0 : Math.Sin(theta);
            transposeMatrix[1, 0] = Math.Abs(-Math.Sign(theta)) < 10e-10 ? 0 : -Math.Sin(theta);
            transposeMatrix[1, 1] = Math.Abs(Math.Cos(theta)) < 10e-10 ? 0 : Math.Cos(theta);
            transposeMatrix[2, 2] = 1;

            transposeMatrix[3, 3] = Math.Abs(Math.Cos(theta)) < 10e-10 ? 0 : Math.Cos(theta);
            transposeMatrix[3, 4] = Math.Abs(Math.Sign(theta)) < 10e-10 ? 0 : Math.Sin(theta);
            transposeMatrix[4, 3] = Math.Abs(-Math.Sign(theta)) < 10e-10 ? 0 : -Math.Sin(theta);
            transposeMatrix[4, 4] = Math.Abs(Math.Cos(theta)) < 10e-10 ? 0 : Math.Cos(theta);
            transposeMatrix[5, 5] = 1;

            return transposeMatrix;

        }

        public Matrix<double> GetLocalStiffnessMatrix()
        {
            /*
            [    EA/L  ,         0       ,       0        ,   -EA/L  ,         0       ,       0       ]      
            [     0    ,    12 EI/L^3    ,   -6 EI/L^2    ,     0    ,   -12 EI/L^3    ,   -6 EI/L^2   ]       
            [     0    ,     6 EI/L^2    ,    4 EI/L      ,     0    ,     6 EI/L^2    ,    2 EI/L     ]      
            [   -EA/L  ,         0       ,      0         ,    EA/L  ,         0       ,      0        ]      
            [     0    ,   -12 EI/L^3    ,    6 EI/L^2    ,     0    ,    12 EI/L^3    ,    6 EI/L^2   ]      
            [     0    ,    -6 EI/L^2    ,    2 EI/L      ,     0    ,     6 EI/L^2    ,    4 EI/L     ]      
            */

            var L = GetMemberLength();
            var kLocal = Matrix<double>.Build.Dense(6, 6);

            var EAOverL = _E * _A / L;
            var EIOverL3 = 12 * _E * _I / Math.Pow(L, 3);
            var EIOverL2 = 6 * _E * _I / Math.Pow(L, 2);
            var EIOverLFour = 4 * _E * _I / L;
            var EIOverLTwo = 2 * _E * _I / L;

            kLocal[0, 0] = EAOverL;
            kLocal[0, 3] = -EAOverL;

            kLocal[1, 1] = EIOverL3;
            kLocal[1, 2] = -EIOverL2;
            kLocal[1, 4] = -EIOverL3;
            kLocal[1, 5] = -EIOverL2;

            kLocal[2, 1] = -EIOverL2;
            kLocal[2, 2] = EIOverLFour;
            kLocal[2, 4] = EIOverL2;
            kLocal[2, 5] = EIOverLTwo;

            kLocal[3, 0] = -EAOverL;
            kLocal[3, 3] = EAOverL;

            kLocal[4, 1] = -EIOverL3;
            kLocal[4, 2] = EIOverL2;
            kLocal[4, 4] = EIOverL3;
            kLocal[4, 5] = EIOverL2;

            kLocal[5, 1] = -EIOverL2;
            kLocal[5, 2] = EIOverLTwo;
            kLocal[5, 4] = EIOverL2;
            kLocal[5, 5] = EIOverLFour;

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
            return GetLocalStiffnessMatrix()* GetLocalDisPlacementVector();
        }
        public void SetMemberForces(Matrix<double> TotalDisplacementVector)
        {
            var Displacement = GetGlobalDisplacementVector();

            var displacementLocal = GetTransposeMatrix() * Displacement;


            //var Displacement = Matrix<double>.Build.Dense(dofPerNode * NumberOfNode, 1);
            //Displacement[0, 0] = TotalDisplacementVector[2 * NodeI.ID - 2, 0];
            //Displacement[1, 0] = TotalDisplacementVector[2 * NodeI.ID - 1, 0];
            //Displacement[2, 0] = TotalDisplacementVector[2 * NodeJ.ID - 2, 0];
            //Displacement[3, 0] = TotalDisplacementVector[2 * NodeJ.ID - 1, 0];

            //var displacementLocal = GetTransposeMatrix() * Displacement;
            //element.IEndDisplacement = displacementLocal[0, 0];
            //element.JEndDisplacement = displacementLocal[1, 0];
            //element.IEndForce = element.E * element.A / element.L * (element.JEndDisplacement - element.IEndDisplacement);
            //element.JEndForce = element.E * element.A / element.L * (element.IEndDisplacement - element.JEndDisplacement);
        }

        #endregion

        #region private Methods

        private double getMemberAngle()
        {
            var NodeI = _nodes[0];
            var NodeJ = _nodes[1];
            return Math.Atan2((NodeJ.Ycoord - NodeI.Ycoord), (NodeJ.Xcoord - NodeI.Xcoord));
        }

        private double GetMemberLength()
        {
            var NodeI = _nodes[0];
            var NodeJ = _nodes[1];
            return Math.Sqrt(Math.Pow(NodeJ.Xcoord - NodeI.Xcoord, 2) + Math.Pow(NodeJ.Ycoord - NodeI.Ycoord, 2));
        }

        private double getMemberAngleAsDegree()
        {
            var NodeI = _nodes[0];
            var NodeJ = _nodes[1];
            return getMemberAngle() * 180 / Math.PI;
        }

        #endregion

    }
}
