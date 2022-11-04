// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Lesson3.ProcessAndThreads;

CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
//ExecutionContext.SuppressFlow();

var asyncLocal = new AsyncLocal<int>();
asyncLocal.Value = 2;
await Task.Delay(5000);
await Task.Delay(5000);
var t = Task.Run(() =>
{
    asyncLocal.Value = 1;
    Console.WriteLine("1: " + asyncLocal.Value);
});
Console.WriteLine("2: " + asyncLocal.Value);
await t;
Console.WriteLine("3: " + asyncLocal.Value + " " + CultureInfo.CurrentCulture.Name);
_ = ThreadPool.UnsafeQueueUserWorkItem(_ =>
{
    Console.WriteLine("4: " + asyncLocal.Value + " " + CultureInfo.CurrentCulture.Name);
}, null);

new Thread(() => Console.WriteLine("5: " + asyncLocal.Value + " " + CultureInfo.CurrentCulture.Name))
    .Start();

Thread.Sleep(100);

return;

{
    await using var file = File.Create("payments.json");
    await JsonSerializer.SerializeAsync(file, PaymentsGenerator.GeneratePayments(5_000_000));
}
return;
Thread.SpinWait(1);
var sw = Stopwatch.StartNew();
await using var file2 = File.OpenRead("payments_50mb.json");
var enumerable = JsonSerializer.Deserialize<IEnumerable<Payment>>(file2);
Console.WriteLine(enumerable.OrderBy(it => it.PaidOn).Count());
// foreach (var payment in enumerable)
// {
//     //Console.WriteLine(sw.Elapsed);
//     //break;
// }
Console.WriteLine(sw.Elapsed);

return;


var res = PaymentsGenerator.GeneratePaymentsParallel(100, l =>
{
    //Console.WriteLine(l);
    Console.WriteLine(Environment.CurrentManagedThreadId);
});
Console.WriteLine(res);
return;

var total = PaymentsGenerator.GenerateSimple(
    "income.txt", "outcome.txt", 5_000_000);

Console.WriteLine($"Total amount: {total:N0}");

var calc = PaymentsCalc.CalcSimple("income.txt", "outcome.txt");
Console.WriteLine($"Verified: {calc:N0}"); //1_000_000