using System;

namespace BenchmarkXamarin.Benchmarks
{
    class BenchmarkExceptions
    {
        [Benchmark]
        public void Simple()
        {
            try
            {
                throw new Exception();
            }
            catch
            {
            }
        }

        [Benchmark]
        public void DivideByZero()
        {
            int zero = 0;
            try
            {
                int result = 100 / zero;
            }
            catch (DivideByZeroException)
            {
            }
        }
    }
}
