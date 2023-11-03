using BenchmarkDotNet.Attributes;
using App.Common;

namespace App.Day5;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class Day5RunBBenchmark
{
    private string Text { get; } = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day5/Day5.input"));

    [Benchmark]
    public void LogicOnly()
    {
        Day5.RunB(Text.AsSpan());
    }
    
    [Benchmark]
    public void LogicAndReadFromDisk()
    {
        Day5.RunB(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day5/Day5.input")));
    }
}