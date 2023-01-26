# Системное программирование
## Вопросы к устному экзамену
1. Зачем нужны DLL?
2. Что такое многопоточность?
3. Что такое асинхронность?
4. Зачем нужна асинхронность?
5. Чем отличается асинхронность от многопоточности?
6. Какие преимущества у тасок (Task) в сравнении с низкоуровневыми потоками (Thread)?
7. Зачем нужен ThreadPool?
8. Что такое Data Race? Приведите примеры.
9. Что такое Deadlock? Приведите примеры.
10. Зачем нужен оператор lock?
11. Что такое потокобезопасный класс?
12. Приведите пример потокоНЕбезопасного типа в C#.
13. Что плохого в использовании синхронных методов?
14. Назовите возможные последствия использования приема "Sync over Async" (`task.Result`, `task.Wait()`)?

## Задачки
1. Что не так с этим кодом?
```csharp
async Task MyMethod()
{
    try
    {
        await WaitAndThrow();
    } catch(Exception e) {
        Console.WriteLine(e);
    }
}

async void WaitAndThrow() {
    await Task.Delay(1);
    throw new Exception();
}
```
2. Что выведет этот код?
```csharp
async Task MyMethod()
{
    try
    {
        WaitAndThrow();
        Console.WriteLine("ОК");
    } catch(Exception e) {
        Console.WriteLine("Ошибка");
    }
}

async Task WaitAndThrow() {
    await Task.Delay(1);
    throw new Exception();
}
```
3. Перед вами код из приложения на WPF. Что с ним не так? И как решить проблему?
```csharp
async void Button_Cick()
{
    string file = await File.ReadAllTextAsync("file.txt").ConfigureAwait(false);
    textBoxContent.Text = file;
}
```
4. Перед вами код из приложения на WPF. Что с ним не так? И как решить проблему?
```csharp
async void Button_Cick()
{
    string file = File.ReadAllTextAsync("file.txt").Result;
    textBoxContent.Text = file;
}
```
5. Перед вами код из приложения на WPF. Что с ним не так? И как решить проблему?
```csharp
async void Button_Cick()
{
    string file = ReadFilesAsync("file1.txt", "file2.txt").Result;
    textBoxContent.Text = file;
}

async Task ReadFilesAsync(string path1, string path2)
{
    var f1 = await File.ReadAllTextAsync(path1);
    var f2 = await File.ReadAllTextAsync(path2);
    return f1 + f2;
}
```

6. Что выведет этот код?
```csharp
var counter = 0;
var tasks = new List<Task>();
for(int i = 0; i < 1000; i++)
{
    tasks.Add(Task.Run(() => counter += 2));
}
await Task.WhenAll(tasks);
Console.Write(counter);
```

7. Что не так с этим кодом и как его починить?
```csharp
var values = {1, 2, 3, 4, 5, 6, 7, 8, 9};
var newValues = new  List<int>();
values
    .AsParallel()
    .ForAll((value) => newValues.Add(value));
```
8. Перед вами код из приложения на WPF. Что с ним не так? И как решить проблему? *
```csharp
async void Button_Cick()
{
    try {
        await File.WriteAllTextAsync("file.txt", textBoxResult).ConfigureAwait(false);
    } catch(Exception e) {
        textBoxResult.Text = "Произошла ошибка";
    }
}
```
9. Как поведет себя ПК при запуске такой программы? Дайте свой прогноз по % ЦП, который будет потреблять такая программа.
```csharp
Parallel.For(0, Environment.ProcessorCount, (_) => { for(;;) ; });
```
10. Как запуск такой программы скажется на производительности ПК? Дайте свой прогноз по % ЦП, который будет потреблять такая программа.
```csharp
Parallel.For(0, Environment.ProcessorCount, (_) => Thread.Sleep(int.MaxValue));
```
11. Как такой код скажется на производительности программы? (что он спровоцирует?)
```csharp
Parallel.For(0, Environment.ProcessorCount, (_) => Thread.Sleep(int.MaxValue));
```
