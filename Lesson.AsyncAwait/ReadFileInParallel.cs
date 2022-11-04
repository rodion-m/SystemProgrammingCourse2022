namespace Lesson.AsyncAwait;

public class ReadFileInParallel
{
    private static readonly string[] s_files =
    {
        @"C:\Users\rodio\Downloads\iconlist_ivtrf.dds",
        @"C:\Users\rodio\Downloads\ПВ121 SP 02 WinAPI.mp4",
        @"C:\Users\rodio\Downloads\Essential Keys-sforzando-v9.6.sf2",
        @"C:\Users\rodio\Downloads\ПВ121 SP 01 Создание DLL на C#.mp4",
        @"C:\Users\rodio\Downloads\Мама альбом 05.05.2022.7z",
        @"C:\Users\rodio\Downloads\webinar_98729465470_0.MP4",
        @"C:\Users\rodio\Downloads\Jade Dynasty server files 2.2.9.7z",
        @"C:\Users\rodio\Downloads\TCPU73.exe",
        @"C:\Users\rodio\Downloads\Chateau Grand-v2.2.sf2",
        @"C:\Users\rodio\Downloads\Score Relief 2021 - Spring Unscored (Composer Arkadiy Mostovoy).mp4",
        @"C:\Users\rodio\Downloads\Nice-Steinway-v3.8.sf2",
        @"C:\Users\rodio\Downloads\Y2Mate.is - Композитор  Александр  МОСТОВОЙ  часть 1-7TE2iyVVbz4-480p-1640771967950.mp4",
        @"C:\Users\rodio\Downloads\dotnet-sdk-6.0.300-win-x64.exe",
        @"C:\Users\rodio\Downloads\BaiduNetdisk_7.14.1.6.exe",
        @"C:\Users\rodio\Downloads\dotnet-sdk-6.0.202-win-x64 (1).exe",
        @"C:\Users\rodio\Downloads\dotnet-sdk-6.0.202-win-x64.exe",
        @"C:\Users\rodio\Downloads\tasks.data",
        @"C:\Users\rodio\Downloads\4bfe6ce44dfb11eca0ac02420a0001b2.pdf",
        @"C:\Users\rodio\Downloads\Teams_windows_x64.exe",
        @"C:\Users\rodio\Downloads\runtime-main.zip",
        @"C:\Users\rodio\Downloads\[веб] Message-driven architecture [2018].zip",
        @"C:\Users\rodio\Downloads\anki-2.1.49-windows.exe",
        @"C:\Users\rodio\Downloads\9. MVC.core-20220525T190447Z-001.zip",
        @"C:\Users\rodio\Downloads\11_6_2021_arhitecture_microservices.mp4",
        @"C:\Users\rodio\Downloads\blocked_sites_24.03.2022.csv",
        @"C:\Users\rodio\Downloads\SF-Pro.dmg",
        @"C:\Users\rodio\Downloads\Александр  Мостовой  Салют  император -OT2tnKMpSY8-480p-1616928065247.mp4",
        @"C:\Users\rodio\Downloads\SpotiFlyer-3.6.1.msi",
        @"C:\Users\rodio\Downloads\Untitled.rar",
        @"C:\Users\rodio\Downloads\Rody.rar",
        @"C:\Users\rodio\Downloads\JabraDirectSetup.exe",
        @"C:\Users\rodio\Downloads\noto-emoji-svg-master.zip",
        @"C:\Users\rodio\Downloads\binance-setup.exe",
        @"C:\Users\rodio\Downloads\FigmaSetup.exe",
        @"C:\Users\rodio\Downloads\AmneziaVPN_2.0.8.3_x64.exe",
        @"C:\Users\rodio\Downloads\LogiCameraSettings_2.12.8.exe",
        @"C:\Users\rodio\Downloads\MovaviVideoSuiteSetup_18_4_0.exe",
        @"C:\Users\rodio\Downloads\Berak_Shkola_ritma_chast_2_Tryokhdolnost.pdf",
        @"C:\Users\rodio\Downloads\Olive-ab31df36-Windows-x86_64.zip",
        @"C:\Users\rodio\Downloads\Учебник. Microsoft ASP.NET Core.pdf",
        @"C:\Users\rodio\Downloads\Magamaev Nokturn (minus 7).wav"
    };

    public static void ReadAllBytesInParallel()
    {
        //первый запуск на холодную: 00:00:10.7885202, последующие: 00:00:05.5486957
        s_files.AsParallel().ForAll(fn => File.ReadAllBytes(fn));
    }
    
    public static void ReadAllBytesSerial()
    {
        //1-й запуск на холодну: 00:00:26.4809998
        //1: 00:00:12.2963745, 2: 00:00:15.4718888
        foreach (var file in s_files)
        {
            File.ReadAllBytes(file);
        }
    }
    
    public static void ReadLinesInParallel()
    {
        //1: 479 мс, 2: 279 мс
        var dir = @"C:\Users\rodio\Downloads\Толстой Л. Н. Полное собрание сочинений в 90 томах (1928-1958)";
        var files = Directory.EnumerateFiles(dir);
        files.AsParallel().ForAll(fn => _ = File.ReadLines(fn).Count());
        
    }
    
    public static void ReadLines(CancellationToken token = default)
    {
        //639 мс.
        var dir = @"C:\Users\rodio\Downloads\Толстой Л. Н. Полное собрание сочинений в 90 томах (1928-1958)";
        var files = Directory.EnumerateFiles(dir);
        _ = files.Select(fn => _ = File.ReadLines(fn).Count()).ToArray();
    }
}