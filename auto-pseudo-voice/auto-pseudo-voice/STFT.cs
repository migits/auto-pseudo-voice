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
        private readonly float[] x, w;
        private readonly int N, nPerSeg, step;
        private readonly float wNorm;
        public STFTIterator(float[] x, float[] window, int step)
        {
            this.x = x;
            this.w = window;
            this.N = x.Length;
            this.nPerSeg = w.Length;
            this.step = step;
            this.wNorm = w.Sum()/this.nPerSeg;
            
            if ((N-this.nPerSeg) % this.step != 0) {
                throw new ArgumentException();
            }
        }

        public IEnumerator<Complex32[]> GetEnumerator()
        {
            int resultLen = this.nPerSeg/2 + 1;
            var buffer = new float[2*resultLen];
            for (int offset = 0; offset < N; offset += this.step)
            {
                for (int n = 0; n < this.nPerSeg; n++) {
                    buffer[n] = this.w[n] * this.x[offset+n];
                }
                Fourier.ForwardReal(buffer, this.nPerSeg, FourierOptions.NoScaling);

                var result = new Complex32[resultLen];
                for (int k = 0; k < resultLen; k++) {
                    result[k] = (new Complex32(buffer[2*k], buffer[2*k+1]))*2/this.nPerSeg
                        / this.wNorm;
                }
                yield return result;
            }
        }
    }
}
