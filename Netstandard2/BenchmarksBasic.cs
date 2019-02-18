namespace BenchmarkXamarin.Sample
{
    public class BenchmarksBasic
    {
        [Benchmark]
        public void Empty()
        {
        }

        [Benchmark]
        public void Assignment()
        {
            int i = 10;
            string s = "Hello World";
            var v = new BenchmarksBasic();
        }

        [Benchmark]
        public void MethodInvocation()
        {
            TestMethod(10);
        }

        private void TestMethod(int i)
        {
            if (i > 0)
                TestMethod(i - 1);
        }
    }
}
