using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Common
{
    public static class MatrixExtension
    {
        public static string ShowMatrixRepresention<T>(this Matrix<double> matrix)
        {
            var text = new StringBuilder();
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    text.Append(matrix[i, j]);
                    text.Append("\t\t\t");
                }
                text.AppendLine();
            }
            return text.ToString();
        }
    }
}
