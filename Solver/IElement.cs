﻿using MathNet.Numerics.LinearAlgebra;
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
        List<ANode> Nodes{ get; }
        double E { get;}

        Matrix<double> GetTransposeMatrix();
        Matrix<double> GetLocalStiffnessMatrix();
        Matrix<double> GetGlobalStiffnessMatrix();
    }
}
