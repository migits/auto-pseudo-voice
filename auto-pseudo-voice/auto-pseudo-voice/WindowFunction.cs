using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace auto_pseudo_voice
{
    static class WindowFunction
    {
        public static float[] hamming(int width)
        {
            return Array.ConvertAll(
                Enumerable.Range(0, width).ToArray(),
                n => Convert.ToSingle(0.54 - 0.46*Math.Cos(2.0*Math.PI*n/width))
            );
        }
    }
}
