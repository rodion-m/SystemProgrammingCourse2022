using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AsyncAwaitAllocation.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 100)]
    public class AsyncAllocationBenchmarks
    {
        [Benchmark]
        public void Benchmark_RealAsyncScenarioYield()
        {
            AsyncAwaitAllocation.Program.RealAsyncScenarioYield().GetAwaiter().GetResult();
        }

        [Benchmark]
        public void Benchmark_RealAsyncScenarioDelay()
        {
            AsyncAwaitAllocation.Program.RealAsyncScenarioDelay().GetAwaiter().GetResult();
        }

        [Benchmark]
        public void Benchmark_RealAsyncScenarioDelayInstantReturn()
        {
            AsyncAwaitAllocation.Program.RealAsyncScenarioDelayInstantReturn().GetAwaiter().GetResult();
        }

        [Benchmark]
        public void Benchmark_QuasiAsyncScenario()
        {
            AsyncAwaitAllocation.Program.QuasiAsyncScenario().GetAwaiter().GetResult();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AsyncAllocationBenchmarks>();
        }
    }
}