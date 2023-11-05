using BenchmarkDotNet.Attributes;
using App.Common;

namespace App.Day8;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class Day8RunBBenchmark
{
    private string Text { get; } = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day8/Day8.input"));

    [Benchmark]
    public void LogicOnly()
    {
        Day8.RunB2(Text);
    }
    
    [Benchmark]
    public void LogicAndReadFromDisk()
    {
        Day8.RunB2(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day8/Day8.input")));
    }
}