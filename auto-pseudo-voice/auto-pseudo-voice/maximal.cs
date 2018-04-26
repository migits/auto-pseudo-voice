using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace auto_pseudo_voice
{
    public static class Maximal
    {
        public static List<int> argrelmax(float[] x, int order=1)
        {
            var result = new List<int>();
            int n = x.Length;

            if (n <= 2) return result;

            bool[] isMaximal = Enumerable.Repeat<bool>(true, n).ToArray();
            
            float[] left  = Enumerable.Repeat<float>(x[n-1], n).ToArray();
            float[] right = Enumerable.Repeat<float>(x[  0], n).ToArray();

            for (int i = order; i > 0; i--)
            {
                Array.Copy(x, i, left , 0, n-i);
                Array.Copy(x, 0, right, i, n-i);

                for (int j = 0; j < n; j++)
                {
                    isMaximal[j] &= left[j] < x[j] && right[j] < x[j];
                }
            }
            
            for (int i = 0; i < n; i++)
            {
                if (isMaximal[i]) result.Add(i);
            }

            return result;
        }
    }
}
