using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace auto_pseudo_voice
{
    class STFTIterator
    {
        private readonly double[] x, w;
        private readonly int N,M,step;
        public STFTIterator(double[] x, double[] w, int step)
        {
            this.w = w;
            this.N = x.Length;
            this.M = w.Length;
            this.step = step;

            this.x = new double[((N-1)/M + 1)*M];
        }

        public IEnumerator<double[]> GetEnumerator()
        {
            var piece = new double[M];
            for (int n = 0; n < N; n += step)
            {
                Array.Copy(this.x, n, piece, 
                yield ForwardReal();
            }
        }
    }
}
