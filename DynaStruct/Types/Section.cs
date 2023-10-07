using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types
{
    public class Section
    {
        #region Ctor

        public Section(double area, double momentOfInertia)
        {
            _area = area;
            _momentOfInertia = momentOfInertia;
        }

        #endregion

        #region Private Fields

        private double _area;
        private double _momentOfInertia;

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"Area: {_area}, " +
                   $"Moment of Inertia: {_momentOfInertia}";
        }

        #endregion
    }

}
