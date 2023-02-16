AllocateOnStack();
AllocateOnStack();
AllocateOnHeap();
Console.ReadKey();

for (int i = 0; i < 50; i++)
{
    await Task.Run(() =>
    {
        AllocateOnStack();
        AllocateOnStack();
        AllocateOnStack();
        AllocateOnStack();
        AllocateOnStack();
    });
    await Task.Delay(500);
}
Console.WriteLine("Done");


Console.ReadKey();
new Thread(() =>
{
    AllocateOnStack();
    Thread.Sleep(1000000);
}, int.MaxValue).Start();
Console.ReadKey();

void AllocateOnStack()
{
    var _2mb = 1024 * 1024 * 1;
    Span<byte> bytes = stackalloc byte[(int)_2mb];
    for (int i = 0; i < bytes.Length; i++)
    {
        bytes[i] = (byte)(i % 255);
    }
    Console.WriteLine("Allocated");
    //Thread.Sleep(10000);
    Console.WriteLine(bytes.Length);
}

void AllocateOnHeap()
{
    var bytes = new byte[1024 * 1024 * 500];
    for (int i = 0; i < bytes.Length; i++)
    {
        bytes[i] = (byte) 'X';
    }
    Console.WriteLine(bytes.Length);
    Console.ReadKey();
}