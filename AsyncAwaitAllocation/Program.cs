﻿namespace AsyncAwaitAllocation;

public class Program
{
    static async Task Main(string[] args)
    {
        var res = await DownloadFileAsync();
        return;
        
        Console.WriteLine($"{nameof(Main)} {Environment.CurrentManagedThreadId}");
        for (int i = 0; i < 1_000; i++)
        {
            await RealAsyncScenarioDelayInstantReturn();
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }

    public static async Task RealAsyncScenarioYield()
    {
        await Task.Yield();
        //Console.WriteLine($"{nameof(RealAsyncScenarioYield)}: {Environment.CurrentManagedThreadId}");
    }
    
    public static async Task RealAsyncScenarioDelay()
    {
        await Task.Delay(0);
        //Console.WriteLine($"{nameof(RealAsyncScenarioDelay)}: {Environment.CurrentManagedThreadId}");
    }
    
    public static Task RealAsyncScenarioDelayInstantReturn()
    {
        return Task.Delay(0);
        //Console.WriteLine($"{nameof(RealAsyncScenarioWithInstantReturn)}: {Environment.CurrentManagedThreadId}");
    }
    
    public static async Task QuasiAsyncScenario()
    {
        for (int i = 0; i < 10; i++)
        {
        }
        //Console.WriteLine($"{nameof(QuasiAsyncScenario)}: {Environment.CurrentManagedThreadId}");
    }
    
    private static readonly HttpClient _httpClient = new();
    public static async Task<byte[]> DownloadFileAsync()
    {
        return await _httpClient.GetByteArrayAsync("https://github.com/dotnet/eShop/raw/main/.gitattributes");
    }
    
    public static Task<byte[]> DownloadFileAsyncInstantReturn()
    {
        return _httpClient.GetByteArrayAsync("https://github.com/dotnet/eShop/raw/main/.gitattributes");
    }
}