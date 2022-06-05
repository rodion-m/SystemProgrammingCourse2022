namespace Lesson3.ProcessAndThreads;

public class X5
{
    public static long ReceivePaymentsParallel(Action<long> nextCallback)
        => PaymentsGenerator.GeneratePaymentsParallel(1_000_000, nextCallback);
}