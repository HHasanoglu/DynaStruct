using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types
{
    public class Material
    {
        #region Ctor

        public Material(double youngsModulus, double yieldStrength)
        {
            _youngsModulus = youngsModulus;
            _yieldStrength = yieldStrength;
        }

        #endregion

        #region Private fields

        private double _youngsModulus;
        private double _yieldStrength;

        #endregion

        #region Properties
        public double YoungsModulus { get => _youngsModulus; set => _youngsModulus = value; }
        public double YieldStrength { get => _yieldStrength; set => _yieldStrength = value; }
        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"Young's Modulus: {_youngsModulus}, " +
                   $"Yield Strength: {_yieldStrength}";
        }

        #endregion
    }

}
