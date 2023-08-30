using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public interface IElement
    {
        int Id { get;}
        double E { get;}
        List<ANode> Nodes{ get; }

        Matrix<double> GetTransposeMatrix();
        Matrix<double> GetLocalStiffnessMatrix();
        Matrix<double> GetGlobalStiffnessMatrix();
        Matrix<double> GetGlobalDisplacementVector();
        Matrix<double> GetLocalDisPlacementVector();
        Matrix<double> GetLocalForceVector();
    }
}
