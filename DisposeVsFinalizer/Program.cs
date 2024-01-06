using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ObjectLayoutInspector;

Methods.RunTimer();
Console.WriteLine("Timer has started");
Console.WriteLine("Press enter to run GC.Collect()");
Console.ReadLine();
GC.Collect(0); // После запуска GC таймер диспозится, т.к. больше нет ссылок на него. Хотя, метод диспоуз явно не вызывался.
Console.WriteLine("GC.Collect() has been called");
Console.Read();

public static class Methods
{
    public static IntPtr TimerPointer;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static unsafe void RunTimer()
    {
        var timer = new Timer(_ 
            => Console.WriteLine(DateTime.Now), null, 0, 1000);
        
        TypeLayout.PrintLayout(timer.GetType());
        Console.WriteLine("Generation of the timer a: " + GC.GetGeneration(timer));
    }
    
}