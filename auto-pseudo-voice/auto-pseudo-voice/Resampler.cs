using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.IntegralTransforms;

namespace auto_pseudo_voice
{
    static class Resampler
    {
        public static float[] resample(float[] x, int M) {
            int N = x.Length;
            var buffer = new float[2*(Math.Max(M, N)/2 + 1)];
            Array.Copy(x, buffer, N);
            Fourier.ForwardReal(buffer, N, FourierOptions.NoScaling);
            Fourier.InverseReal(buffer, M, FourierOptions.NoScaling);
            var result = new float[M];
            for (int k = 0; k < M; k++) {
                result[k] = buffer[k]/N;
            }
            return result;
        }
    }
}
