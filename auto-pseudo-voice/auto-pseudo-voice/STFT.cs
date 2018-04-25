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
        public STFTIterator(float[] x, float[] window)
        {
            this.x = x;
            this.w = window;
            this.N = x.Length;
            this.nPerSeg = w.Length;
            this.step = this.nPerSeg/2;

            if (this.nPerSeg % 2 != 0) {
                throw new ArgumentException();
            }
            if ((N-this.nPerSeg) % this.step != 0) {
                throw new ArgumentException();
            }
        }

        public IEnumerator<Complex32[]> GetEnumerator()
        {
            int resultLen = this.nPerSeg/2 + 1;
            var buffer = new float[2*resultLen];
            for (int n = 0; n <= N-this.nPerSeg; n += this.step)
            {
                for (int i = 0; i < this.nPerSeg; i++) {
                    float a = (float)i/this.nPerSeg;
                    buffer[i] = 2*(i<this.step ? a : 1-a) * this.x[n+i];
                }
                Fourier.ForwardReal(buffer, this.nPerSeg);

                var result = new Complex32[resultLen];
                for (int i = 0; i < resultLen; i++) {
                    result[i] = new Complex32(buffer[2*i], buffer[2*i+1]);
                }
                yield return result;
            }
        }
    }
}
