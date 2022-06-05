namespace Lesson3.ProcessAndThreads;

public class PaymentsCalc
{
    public static long CalcSimple(string incomeFileName, string outcomeFileName)
    {
        var income = EnumerateLines(incomeFileName).Select(long.Parse).Sum();
        var outcome = EnumerateLines(outcomeFileName).Select(long.Parse).Sum();
        return income - outcome;
    }

    public static IEnumerable<string> EnumerateLines(string fileName)
    {
        using var reader = File.OpenText(fileName);
        for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            yield return line;
        }
    }
}