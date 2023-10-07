using System;
using System.Collections.Generic;
using System.Text;

namespace Modeling
{
    public class Point3D
    {
        #region Ctor

        public Point3D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Properties

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        #endregion
    }

}
