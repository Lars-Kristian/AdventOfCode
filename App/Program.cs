using System.Diagnostics;
using BenchmarkDotNet.Running;
using App.Day4;
using App.Day6;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;


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
            Console.WriteLine(Day4.RunB(text));
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

//HelloWorldGenerated.HelloWorld.SayHello();

modeSetup[Modes.Day4B]();

GeneratedRunMethods.GeneratedRunForRunB();

public enum Modes
{
    Default = 0,
    Day4A,
    Day4B,
    Day4BBenchmark
};