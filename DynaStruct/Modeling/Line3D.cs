using System;
using System.Collections.Generic;
using System.Text;

namespace Modeling
{
    public class Line3D
    {
        #region Ctor

        public Line3D(Guid Id,Point3D startPoint, Point3D endPoint)
        {
            ID=Id;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        #endregion

        #region Properties

        public Point3D StartPoint { get; }
        public Point3D EndPoint { get; }
        public Guid ID { get; }

        #endregion

        #region Public Methods

        public double CalculateLength()
        {
            double deltaX = EndPoint.X - StartPoint.X;
            double deltaY = EndPoint.Y - StartPoint.Y;
            double deltaZ = EndPoint.Z - StartPoint.Z;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"Start Point: ({StartPoint.X}, {StartPoint.Y}, {StartPoint.Z}), " +
                   $"End Point: ({EndPoint.X}, {EndPoint.Y}, {EndPoint.Z}), " +
                   $"Length: {CalculateLength()}";
        }

        #endregion
    }
}
