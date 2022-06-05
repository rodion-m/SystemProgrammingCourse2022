// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text.Json;
using Lesson3.ProcessAndThreads;

// {
//     await using var file = File.Create("payments.json");
//     await JsonSerializer.SerializeAsync(file, PaymentsGenerator.GeneratePayments(100_000));
// }
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