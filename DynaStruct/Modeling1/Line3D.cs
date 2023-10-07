using System;
using System.Collections.Generic;
using System.Text;
using Types;

namespace Modeling
{
    public class Line3D
    {
        #region Ctor

        public Line3D(Guid id, Point3D startPoint, Point3D endPoint)
        {
            _id = id;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        #endregion

        #region Properties

        public Guid Id { get => _id; set => _id = value; }
        public Point3D StartPoint { get => _startPoint; set => _startPoint = value; }
        public Point3D EndPoint { get => _endPoint; set => _endPoint = value; }
        public Material Material { get => _material; set => _material = value; }
        public Section Section { get => _section; set => _section = value; }

        #endregion

        #region Private Fields

        private Guid _id;
        private Point3D _startPoint;
        private Point3D _endPoint;
        private Material _material;
        private Section _section;

        #endregion

        #region Public Methods

        public double GetLength()
        {
            double deltaX = _endPoint.X - _startPoint.X;
            double deltaY = _endPoint.Y - _startPoint.Y;
            double deltaZ = _endPoint.Z - _startPoint.Z;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"Start Point: ({_startPoint.X}, {_startPoint.Y}, {_startPoint.Z}), " +
                   $"End Point: ({_endPoint.X}, {_endPoint.Y}, {_endPoint.Z}), " +
                   $"Length: {GetLength()}";
        }

        #endregion
    }
}
