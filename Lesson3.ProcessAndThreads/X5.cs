namespace Lesson3.ProcessAndThreads;

public class X5
{
    private static long s_lastValue = 0;
    public static void ReceivePaymentsParallel(Action<long> nextCallback)
    {
        s_lastValue = PaymentsGenerator.GeneratePaymentsParallel(5_000_000, nextCallback);
    }

    public long GetLastResult() => s_lastValue;
}