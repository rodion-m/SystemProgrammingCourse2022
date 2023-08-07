using System.Diagnostics;
using Lesson.AsyncAwait;

Console.WriteLine("Перед sync:");
Console.WriteLine(Environment.CurrentManagedThreadId);

var semaphoreSlim = new SemaphoreSlim(2, 2);

await Parallel.ForEachAsync(Enumerable.Range(0, 10), async (i, _) =>
{
    await semaphoreSlim.WaitAsync(_); //lock (thread #1)
    
    Console.WriteLine($"Операция #: {i}");
    Console.WriteLine($"ID потока: {Environment.CurrentManagedThreadId}");
    await Task.Delay(5000);
    Console.WriteLine($"Операция {i} завершена");
    semaphoreSlim.Release();
});

// var sw1 = Stopwatch.StartNew();
// ReadFileInParallel.ReadAllBytesSerial();
//
// Console.WriteLine(sw1.Elapsed);
//
// return;
//
//
// //ThreadPool.GetMinThreads(out var w, out var completionPortThreads);
// {
//     ThreadPool.GetAvailableThreads(out var w, out var completionPortThreads);
//     Console.WriteLine($"{w} {completionPortThreads}");
// }
//
// {
//     var t1 = File.ReadAllBytesAsync(@"C:\Games\Perfect World\element\models.pkx");
//     var t2 = File.ReadAllBytesAsync(@"C:\Games\Perfect World\element\models.pkx");
//     var t3 = File.ReadAllBytesAsync(@"C:\Games\Perfect World\element\models.pkx");
//     Thread.Sleep(500);
//     Console.WriteLine(ThreadPool.ThreadCount);
//     ThreadPool.GetAvailableThreads(out var workerThreads, out var completionPortThreads);
//     Console.WriteLine($"{workerThreads} {completionPortThreads}");
// }
// return;
//
// var tasks = new List<Task>();
// for (int i = 0; i < 1_000_001; i++)
// {
//     var ii = i;
//     tasks.Add(Task.Run(async () =>
//     {
//         if (ii % 1000 == 0)
//             Console.WriteLine(ii);
//         await Task.Delay(10000);
//     }));
// }
//
// var sw = Stopwatch.StartNew();
// await Task.WhenAll(tasks);
// Console.WriteLine(sw.ElapsedMilliseconds);
// return;
//
// var numbers = new[] { 0, 1 };
// int result = await Task.Run(() =>
// {
//     int total = 0;
//     foreach (var n in numbers)
//     {
//         total += n;
//     }
//
//     return total;
// });
//
//
// Task<string> task1 = File.ReadAllTextAsync("file1.txt");
// Task<string> task2 = File.ReadAllTextAsync("file2.txt");
// // task1.Wait();
// // task2.Wait();
// // string file1Content = await task1;
// // string file2Content = await task2;
// // textBox.Text = file1Content + file2Content;
//
// string[] texts = await Task.WhenAll(task1, task2);
// string file1Content = texts[0];
// string file2Content = texts[1];
//
