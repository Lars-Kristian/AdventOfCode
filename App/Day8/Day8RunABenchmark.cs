using BenchmarkDotNet.Attributes;
using App.Common;

namespace App.Day8;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class Day8RunABenchmark
{
    private string Text { get; } = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day8/Day8.input"));

    [Benchmark]
    public void LogicOnly()
    {
        Day8.RunA(Text.AsSpan());
    }
    
    [Benchmark]
    public void LogicAndReadFromDisk()
    {
        Day8.RunA(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day8/Day8.input")));
    }
}