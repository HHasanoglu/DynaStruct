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
                    if (matrix[i, j]>0)
                    {
                        text.Append(" ");
                    }
                    text.Append(matrix[i, j].ToString("#0.00#"));
                    text.Append("\t\t\t");
                }
                text.AppendLine();
            }
            return text.ToString();
        }
    }
}
