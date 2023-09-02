using MathNet.Numerics.LinearAlgebra;
using Solver.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Assembler
    {
        #region Ctor
        public Assembler(List<IElement> ElementList, List<ANode> Nodes)
        {
            var KGlobal = getAssembleMatrix(Nodes, ElementList);
            Matrix<double> KGReduced = getAssembledReducedMatrix(Nodes, ElementList);
            KGReduced.ShowMatrixRepresention<double>();
            var forceReduced = GetReducedForceVector(Nodes);
            var displacement = GetDisplacementVector(KGReduced, forceReduced);
            var TotalDisplacementVector = GetTotalDisplacement(Nodes, displacement);
            var reactions = GetReactions(KGlobal, TotalDisplacementVector);
        }

        #endregion

        #region Private Regions

        #endregion

        #region private Methods

        #endregion
        private string showMatrixRepresention(Matrix<double> matrix)
        {
            var text = new StringBuilder();
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    text.Append(matrix[i, j]);
                    text.Append("\t");
                }
                text.AppendLine();
            }
            return text.ToString();
        }

        private Matrix<double> GetReactions(Matrix<double> KG, Matrix<double> displacementsTotal)
        {
            return KG * displacementsTotal;
        }

        private Matrix<double> getAssembleMatrix(List<ANode> Nodes, List<IElement> ElementsList)
        {
            var dofPerNode = Nodes[0].DofPerNode;
            var Ndof = Nodes.Count * dofPerNode;
            var KG = Matrix<double>.Build.Dense(Ndof, Ndof);
            var arr2d = GetMappingArrayPrimary(Nodes);
            var G = new int[2 * dofPerNode];

            //var sorted = StiffnessMatrixList.Ordw(x => x.StartNode);
            var kk = 0;
            foreach (IElement element in ElementsList)
            {
                kk++;
                Matrix<double> Kg = element.GetGlobalStiffnessMatrix();
                for (int i = 0; i < dofPerNode; i++)
                {
                    G[i] = arr2d[element.Nodes[0].ID - 1, i];
                    G[i + dofPerNode] = arr2d[element.Nodes[1].ID - 1, i];
                }

                for (int i = 0; i < 2 * dofPerNode; i++)
                {
                    var P = G[i];
                    for (int j = 0; j < 2 * dofPerNode; j++)
                    {
                        var Q = G[j];
                        KG[P, Q] = KG[P, Q] + Kg[i, j];
                    }
                }
            }
            return KG;
        }

        private Matrix<double> getAssembledReducedMatrix(List<ANode> Nodes, List<IElement> ElementsList)
        {
            GetMappingArray(out int[,] arr2d, out int count, Nodes);
            var dofPerNode = Nodes[0].DofPerNode;
            int[] G = new int[2 * dofPerNode];

            var KGReduced = Matrix<double>.Build.Dense(count, count);
            foreach (IElement element in ElementsList)
            {
                for (int i = 0; i < dofPerNode; i++)
                {
                    G[i] = arr2d[element.Nodes[0].ID - 1, i];
                    G[i + dofPerNode] = arr2d[element.Nodes[1].ID - 1, i];
                }

                Matrix<double> Kg = element.GetGlobalStiffnessMatrix();
                for (int i = 0; i < 2 * dofPerNode; i++)
                {
                    for (int j = 0; j < 2 * dofPerNode; j++)
                    {
                        var P = G[i];
                        var Q = G[j];
                        if (P != -1 && Q != -1)
                        {
                            KGReduced[P, Q] = KGReduced[P, Q] + Kg[i, j];
                        }
                    }
                }
            }
            return KGReduced;
        }


        private Matrix<double> GetReducedForceVector(List<ANode> Nodes)
        {
            var dofPerNode = Nodes[0].DofPerNode;
            GetMappingArray(out int[,] arr2d, out int count, Nodes);

            var forceReduced = Matrix<double>.Build.Dense(count, 1);

            foreach (ANode node in Nodes)
            {
                for (int i = 0; i < dofPerNode; i++)
                {
                    var Q = arr2d[node.ID - 1, i];
                    if (Q != -1)
                    {
                        switch (i)
                        {
                            case 0:

                                forceReduced[Q, 0] = node.Fx;
                                break;
                            case 1:

                                forceReduced[Q, 0] = node.Fy;
                                break;
                        }
                    }
                }
            }
            return forceReduced;
        }

        private void GetMappingArray(out int[,] arr2d, out int count, List<ANode> Nodes)
        {
            var dofPerNode = Nodes[0].DofPerNode;
            arr2d = new int[Nodes.Count, Nodes[0].DofPerNode];
            foreach (ANode node in Nodes)
            {
                var rowID = node.ID;
                if (node.XRestraint == eRestraint.Restrained) arr2d[rowID - 1, 0] = -1;
                if (node.YRestraint == eRestraint.Restrained) arr2d[rowID - 1, 1] = -1;
                if (node is FrameNode)
                {
                    if (((FrameNode)node).RotationRestraint == eRestraint.Restrained) arr2d[rowID - 1, 2] = -1;
                }
            }
            count = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < dofPerNode; j++)
                {
                    if (arr2d[i, j] == 0)
                    {
                        arr2d[i, j] = count;
                        count += 1;
                    }
                }
            }
        }

        private int[,] GetMappingArrayPrimary(List<ANode> Nodes)
        {
            var dofPerNode = Nodes[0].DofPerNode;
            int[,] arr2d = new int[Nodes.Count, Nodes[0].DofPerNode];
            var count = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < dofPerNode; j++)
                {
                    arr2d[i, j] = count;
                    count += 1;
                }
            }
            return arr2d;
        }
        private Matrix<double> GetDisplacementVector(Matrix<double> KGReduced, Matrix<double> forceReduced)
        {

            return KGReduced.Inverse() * forceReduced;
        }

        private Matrix<double> GetTotalDisplacement(List<ANode> Nodes, Matrix<double> displacements)
        {
            var dofPerNode = Nodes[0].DofPerNode;
            GetMappingArray(out int[,] arr2d, out int count, Nodes);
            var NumberOfDof = Nodes.Count * dofPerNode;
            var displacementsTotal = Matrix<double>.Build.Dense(NumberOfDof, 1);

            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < dofPerNode; j++)
                {
                    var Q = arr2d[i, j];
                    if (Q != -1)
                    {
                        var Id = dofPerNode * i + j;
                        displacementsTotal[Id, 0] = displacements[Q, 0];
                        var node = Nodes.FirstOrDefault(x => x.ID == i + 1);
                        if (j == 0)
                        {
                            node.Dispx = displacements[Q, 0];
                        }
                        else if (j == 1)
                        {
                            node.Dispy = displacements[Q, 0];
                        }
                        else
                        {

                            if (node as FrameNode != null)
                            {
                                ((FrameNode)node).Rotation = displacements[Q, 0];
                            }
                        }
                    }
                }
            }
            return displacementsTotal;
        }

    }
}
