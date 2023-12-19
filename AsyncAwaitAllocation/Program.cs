namespace AsyncAwaitAllocation;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"{nameof(Main)} {Environment.CurrentManagedThreadId}");
        for (int i = 0; i < 1_000_000; i++)
        {
            await RealAsyncScenario();
        }
        Console.ReadLine();
    }

    static async Task RealAsyncScenario()
    {
        await Task.Yield();
        //Console.WriteLine($"{nameof(RealAsyncScenario)}: {Environment.CurrentManagedThreadId}");
    }
    
    static async Task QuasiAsyncScenario()
    {
        for (int i = 0; i < 10; i++)
        {
        }
        //Console.WriteLine($"{nameof(QuasiAsyncScenario)}: {Environment.CurrentManagedThreadId}");
    } 
}