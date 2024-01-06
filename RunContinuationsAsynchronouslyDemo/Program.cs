Console.WriteLine($"Main 1: {Environment.CurrentManagedThreadId}");

try
{
    await RunInNewThread(() =>
    {
        Console.WriteLine($"Action: {Environment.CurrentManagedThreadId}");
        throw new InvalidOperationException("Exception in Action");
    }, asyncContinuation: false);
}
catch
{
    Console.WriteLine("Caught!!!");
}

Console.WriteLine($"Main 2: {Environment.CurrentManagedThreadId}");
Thread.Sleep(1000);

Task RunInNewThread(Action action, bool asyncContinuation)
{
    // asyncContinuation для Thread роли не играет, зато для Task "Main 2" будет выполнен в том же потоке, что и "Action"
    var tcs = new TaskCompletionSource(asyncContinuation ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
    var thread = new Thread(() =>
    {
        try
        {
            action();
            tcs.SetResult();
        }
        catch (Exception e)
        {
            tcs.SetException(e);
        }
        finally
        {
            Console.WriteLine($"Finally: {Environment.CurrentManagedThreadId}");
        }
    });
    thread.Start();
    return tcs.Task;
}