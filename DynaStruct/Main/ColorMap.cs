using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESolver
{
    public class ColorMap
    {
        private double min;
        private double max;
        private string name;
        private Color[] colorMap;
        public ColorMap(double min, double max)
        {
            this.min = min;
            this.max = max;
            this.name = "jet";
        }
        public Color GetColor(double value)
        {
            if (value == max)
            {
                return Color.Red;
            }
            else if (value == min)
            {
                return Color.Blue;
            }
            else
            {
                double ratio = (value - min) / (max - min);
                Color[] colors = {Color.Blue , Color.Cyan, Color.Lime, Color.Yellow, Color.Orange,Color.Red
                              //Color.FromArgb(0, 0, 204),
                              //Color.FromArgb(0, 51, 204),
                              //Color.FromArgb(0, 102, 204),
                              //Color.FromArgb(0, 153, 204),
                              //Color.FromArgb(0, 204, 204),
                              //Color.FromArgb(0, 204, 153),
                              //Color.FromArgb(0, 204, 102),
                              //Color.FromArgb(0, 204, 51),
                              //Color.FromArgb(0, 204, 0),
                              //Color.FromArgb(51, 204, 0),
                              //Color.FromArgb(102, 204, 0),
                              //Color.FromArgb(153, 204, 0),
                              //Color.FromArgb(204, 204, 0),
                              //Color.FromArgb(204, 153, 0),
                              //Color.FromArgb(204, 102, 0),
                              //Color.FromArgb(204, 51, 0),
                              //Color.FromArgb(204, 0, 0),
            
            };
                double segmentRatio = 1.0 / (colors.Length - 1);
                // Map the ratio to a segment and calculate the segment-specific ratio
                int segmentIndex = (int)(ratio / segmentRatio);
                double segmentRatioValue = (ratio - segmentIndex * segmentRatio) / segmentRatio;

                // Interpolate between the colors of the current segment and the next segment
                Color startColor = colors[segmentIndex];
                Color endColor = colors[segmentIndex + 1];
                int r = (int)(startColor.R + segmentRatioValue * (endColor.R - startColor.R));
                int g = (int)(startColor.G + segmentRatioValue * (endColor.G - startColor.G));
                int b = (int)(startColor.B + segmentRatioValue * (endColor.B - startColor.B));
                Color interpolatedColor = Color.FromArgb(r, g, b);
                return interpolatedColor;
            }

        }
    }

}
