using System.Diagnostics;

public class Test
{
    public static void Run()
    {
        var sw = Stopwatch.StartNew();
        var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input"));
        Console.WriteLine(App.Day4.Day4.RunB(text));
        sw.Stop();
        Console.WriteLine($"Time used {sw.ElapsedMilliseconds}ms");
    }
}