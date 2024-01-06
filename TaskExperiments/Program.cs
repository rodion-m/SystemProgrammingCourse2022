Console.WriteLine(await ReturnInt()); //проверить кол-во аллокаций
return;

await Task.Run(async () =>
{
    var t = Delay();
    Thread.Sleep(3000);
    Console.WriteLine(await t);
});

Task<int> ReturnInt()
{
    return Task.FromResult(9);
}

async Task<int> Delay()
{
    await File.WriteAllTextAsync("file", "content");
    Console.WriteLine("Delay");
    return 1;
}