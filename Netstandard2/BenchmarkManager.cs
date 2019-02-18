using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BenchmarkXamarin
{
    public class BenchmarkManager
    {
        private const int WarmupCount = 100;
        private const int WorkingCount = 1000;
        private static readonly object[] Args = new object[0];
        private List<MethodInfo> _benchmarks = new List<MethodInfo>();

        public static BenchmarkManager Current { get; } = new BenchmarkManager();

        private BenchmarkManager() { }

        public event LogEventHandler Log = text => { };

        public void Register(Assembly assembly)
        {
            var benchmarks = from t in assembly.DefinedTypes
                             from mi in t.DeclaredMethods
                             where mi.IsPublic && !mi.IsStatic && mi.GetCustomAttribute<BenchmarkAttribute>() != null
                             select mi;
            _benchmarks.AddRange(benchmarks);
        }

        public void Start()
        {
            foreach (MethodInfo benchmark in _benchmarks)
                Perform(benchmark);
        }

        private void Perform(MethodInfo benchmark)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            object obj = null;
            if (benchmark.DeclaringType != null)
                obj = Activator.CreateInstance(benchmark.DeclaringType);

            // warmup
            for (int i = 0; i < WarmupCount; i++)
                Execute(benchmark, obj);

            // work
            var stopwatch = new Stopwatch();
            for (int i = 0; i < WorkingCount; i++)
                Execute(benchmark, obj, stopwatch);

            double elapsed = Convert.ToDouble(stopwatch.ElapsedMilliseconds);
            double average = elapsed / WorkingCount;

            Log(string.Format("{0}: {1}ms", benchmark.Name, average));
        }

        private static void Execute(MethodInfo benchmark, object obj, Stopwatch stopwatch = null)
        {
            if (stopwatch != null)
                stopwatch.Start();

            benchmark.Invoke(obj, Args);

            if (stopwatch != null)
                stopwatch.Stop();
        }
    }
}
