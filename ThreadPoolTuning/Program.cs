// See https://aka.ms/new-console-template for more information

using ThreadPoolTuning;
//PortableThreadPool.cs

var confs = new string[]
{
    "System.Threading.ThreadPool.MaxThreads", 
    "System.Threading.ThreadPool.MinThreads", 
    "System.Threading.ThreadPool.ProcessorsPerIOPollerThread", 
    "System.Threading.ThreadPool.UnfairSemaphoreSpinLimit", 
    "System.Threading.ThreadPool.EnableWorkerTracking",
    "System.Threading.ThreadPool.DisableStarvationDetection", 
    "System.Threading.ThreadPool.DebugBreakOnWorkerStarvation", 
    "System.Threading.ThreadPool.Blocking.CooperativeBlocking", 
    "System.Threading.ThreadPool.Blocking.IgnoreMemoryUsage", 
    "System.Threading.ThreadPool.Blocking.ThreadsToAddWithoutDelay_ProcCountFactor", 
    "System.Threading.ThreadPool.Blocking.ThreadsPerDelayStep_ProcCountFactor", 
    "System.Threading.ThreadPool.Blocking.DelayStepMs", 
    "System.Threading.ThreadPool.Blocking.MaxDelayMs", 
    "System.Threading.ThreadPool.HillClimbing.Disable", 
    "System.Threading.ThreadPool.HillClimbing.WavePeriod", 
    "System.Threading.ThreadPool.HillClimbing.MaxWaveMagnitude", 
    "System.Threading.ThreadPool.HillClimbing.WaveMagnitudeMultiplier", 
    "System.Threading.ThreadPool.HillClimbing.WaveHistorySize", 
    "System.Threading.ThreadPool.HillClimbing.Bias", 
    "System.Threading.ThreadPool.HillClimbing.TargetSignalToNoiseRatio", 
    "System.Threading.ThreadPool.HillClimbing.MaxChangePerSecond", 
    "System.Threading.ThreadPool.HillClimbing.MaxChangePerSample", 
    "System.Threading.ThreadPool.HillClimbing.SampleIntervalLow", 
    "System.Threading.ThreadPool.HillClimbing.SampleIntervalHigh", 
    "System.Threading.ThreadPool.HillClimbing.ErrorSmoothingFactor", 
    "System.Threading.ThreadPool.HillClimbing.GainExponent", 
    "System.Threading.ThreadPool.HillClimbing.MaxSampleErrorPercent",
};
//https://github.com/dotnet/runtime/issues/47922
//AppDomain.CurrentDomain.SetData(configName, true);
Console.WriteLine(ThreadPool.SetMinThreads(Environment.ProcessorCount / 2, 1000));
Console.WriteLine(ThreadPool.SetMaxThreads(Environment.ProcessorCount / 2, 1000));
//ThreadPool.SetMinThreads(Environment.ProcessorCount / 2, 1000);
//ThreadPool.SetMaxThreads(Environment.ProcessorCount / 2, 1000);
foreach (var confName in confs)
{
    object? value = AppContext.GetData(confName);
    Console.WriteLine($"{confName}: {value}");
}

//PortableThreadPool.IO.Windows.cs
// static int GetIOCompletionPollerCount()
// {
//     // Named for consistency with SocketAsyncEngine.Unix.cs, this environment variable is checked to override the exact
//     // number of IO completion poller threads to use. See the comment in SocketAsyncEngine.Unix.cs about its potential
//     // uses. For this implementation, the ProcessorsPerIOPollerThread config option below may be preferable as it may be
//     // less machine-specific.
//     if (uint.TryParse(Environment.GetEnvironmentVariable("DOTNET_SYSTEM_NET_SOCKETS_THREAD_COUNT"), out uint count))
//     {
//         return Math.Min((int)count, MaxPossibleThreadCount);
//     }
//
//     if (UnsafeInlineIOCompletionCallbacks)
//     {
//         // In this mode, default to ProcessorCount pollers to ensure that all processors can be utilized if more work
//         // happens on the poller threads
//         return Environment.ProcessorCount;
//     }
//
//     int processorsPerPoller =
//         AppContextConfigHelper.GetInt32Config("System.Threading.ThreadPool.ProcessorsPerIOPollerThread", 12, false);
//     return (Environment.ProcessorCount - 1) / processorsPerPoller + 1;
// }