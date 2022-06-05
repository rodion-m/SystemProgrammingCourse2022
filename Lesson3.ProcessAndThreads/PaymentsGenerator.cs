namespace Lesson3.ProcessAndThreads;

internal class PaymentsGenerator
{
    public long Total => _total;

    private long _total;
    private const int l1m = 1_000_000;

    /// <summary>
    /// Возвращает итог
    /// </summary>
    /// <returns>Возвращает итог</returns>
    public static long GenerateSimple(string incomeFileName, string outcomeFileName, int maxCount)
    {
        int n = 0;
        var generator = new PaymentsGenerator();
        Parallel.Invoke(
            () => GenerateFile(incomeFileName, false),
            () => GenerateFile(outcomeFileName, true)
        );

        void GenerateFile(string fileName, bool isOutcome)
        {
            using var sw = File.CreateText(fileName);
            while (n < maxCount)
            {
                sw.WriteLine(generator.GetNextPayment(isOutcome));
                Interlocked.Increment(ref n);
            }
        }

        var total = generator.Total;
        var remainder = total % l1m;
        File.AppendAllLines(outcomeFileName, new[] { remainder.ToString() });
        total -= remainder;
        if (total < 0)
        {
            total += l1m;
            File.AppendAllLines(incomeFileName, new[] { l1m.ToString() });
        }

        return total;
    }

    public IEnumerable<long> GeneratePaymentsSimple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return GetNextPayment(i % 2 == 1);
        }
    }

    public static IEnumerable<Payment> GeneratePayments(int count)
    {
        var generator = new PaymentsGenerator();
        var minDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var maxDate = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)
            .AddSeconds(-1);
        return generator.GeneratePaymentsSimple(count).Select(amount =>
        {
            long randomTime = Random.Shared.NextInt64(minDate.ToUnixTimeSeconds(), maxDate.ToUnixTimeSeconds());
            return new Payment(
                DateTimeOffset.FromUnixTimeSeconds(randomTime),
                amount
            );
        });
    }

    public static long GeneratePaymentsParallel(int maxCount, Action<long> nextCallback)
    {
        var generator = new PaymentsGenerator();
        generator.GeneratePaymentsSimple(maxCount)
            .AsParallel()
            .ForAll(nextCallback);

        var total = generator.Total;
        var remainder = total % l1m;
        nextCallback(-remainder);
        total -= remainder;
        if (total < 0)
        {
            total += l1m;
            nextCallback(l1m);
        }

        return total;
    }

    public long GetNextPayment(bool isOutcome)
    {
        long amount = 0;
        var isIncome = !isOutcome;
        do
        {
            var next = Random.Shared.NextInt64(l1m);
            amount += next;
            Interlocked.Add(ref _total, isOutcome ? -next : next);
        } while ((isOutcome && Interlocked.Read(ref _total) > l1m * 10)
                 || (isIncome && Interlocked.Read(ref _total) < 0));

        return amount;
    }
}