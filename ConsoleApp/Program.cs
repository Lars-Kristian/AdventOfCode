using System.Diagnostics;
using BenchmarkDotNet.Running;
using ConsoleApp.Day4;
using ConsoleApp.Missions;


var modeSetup = new Dictionary<Modes, Action>()
{
    {
        Modes.Day4A, 
        () =>
        {
            var sw = Stopwatch.StartNew();
            var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input"));
            Console.WriteLine(Day4.RunA(text));
            sw.Stop();
            Console.WriteLine($"Time used {sw.ElapsedMilliseconds}ms");
        }
    },
    {
        Modes.Day4B, 
        () =>
        {
            var sw = Stopwatch.StartNew();
            var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input"));
            Console.WriteLine(Day4.RunBTest(text));
            sw.Stop();
            Console.WriteLine($"Time used {sw.ElapsedMilliseconds}ms");
        }
    },
    {
        Modes.Day4BBenchmark, 
        () => { BenchmarkRunner.Run<Day4Benchmark>(); }
    },
};

/*
var modes = new List<Action>()
{
    () =>
    {
        var sw = Stopwatch.StartNew();
        var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Missions/Day6.input"));
        Console.WriteLine(Day6.RunB(text));
        sw.Stop();
        Console.WriteLine($"Time used {sw.ElapsedMilliseconds}ms");
    },
    () => { BenchmarkRunner.Run<Day6Benchmark>(); }
};
*/

modeSetup[Modes.Day4BBenchmark]();

public enum Modes
{
    Default = 0,
    Day4A,
    Day4B,
    Day4BBenchmark
};