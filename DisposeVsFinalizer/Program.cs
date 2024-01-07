using System.Runtime.CompilerServices;
using JetBrains.Profiler.Api;
using ObjectLayoutInspector;

bool enableProfiler = true;
if (enableProfiler)
{
    /*
     * To enable the API, you must start a profiling session with the enabled API:
     * In Rider goto:
     *  Run - Switch Profiling Configuration - Edit Configurations - Control Profiling via API
     * https://www.jetbrains.com/help/dotmemory/Profiling_Guidelines__Advanced_Profiling_Using_dotTrace_API.html#profiling-a-specific-part-of-the
     */
    MemoryProfiler.CollectAllocations(true);
    MemoryProfiler.GetSnapshot("Before running the timer");
}

Methods.RunTimer();
if (enableProfiler)
{
    MemoryProfiler.GetSnapshot("After running the timer");
}
Console.WriteLine("Timer has started");
Console.WriteLine("Press enter to run GC.Collect()");
Console.ReadLine();
GC.Collect(); // После запуска GC таймер диспозится, т.к. больше нет ссылок на него. Хотя, метод диспоуз явно не вызывался.
if (enableProfiler)
{
    MemoryProfiler.GetSnapshot("After the first call of GC.Collect()");
}
Console.WriteLine("GC.Collect() has been called");
Console.WriteLine("Press enter to run GC.Collect() again");
GC.Collect();
if (enableProfiler)
{
    MemoryProfiler.GetSnapshot("After the second call of GC.Collect()");
}
Console.Read();

public static class Methods
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static unsafe void RunTimer()
    {
        Console.WriteLine(GC.GetGCMemoryInfo().MemoryLoadBytes);
        var timer = new Timer(_ 
            => Console.WriteLine(DateTime.Now), null, 0, 1000);
        Console.WriteLine(GC.GetGCMemoryInfo().MemoryLoadBytes);
        
        TypeLayout.PrintLayout(timer.GetType());
        // Console.WriteLine("Generation of the timer a: " + GC.GetGeneration(timer));
        // GC.Collect(0);
        // Console.WriteLine(GC.GetGCMemoryInfo().MemoryLoadBytes);
        // Console.WriteLine("Generation of the timer b: " + GC.GetGeneration(timer));
        // GC.Collect(1);
        // Console.WriteLine("Generation of the timer c: " + GC.GetGeneration(timer));
        // GC.Collect(2);
        // Console.WriteLine("Generation of the timer d: " + GC.GetGeneration(timer));
    }
    
}