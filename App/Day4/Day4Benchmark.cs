using BenchmarkDotNet.Attributes;
using App.Common;

namespace App.Day4;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class Day4Benchmark
{
    private string Text { get; } = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input"));
    private string FilePath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input");

    [Benchmark]
    public void LogicOnly()
    {
        Day4.RunB(Text.AsSpan());
    }
    
    [Benchmark]
    public void LogicAndReadFromDisk()
    {
        Day4.RunB(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4/Day4.input")));
    }
    
    [Benchmark]
    public void LogicOnlyTest()
    {
        Day4.RunBTest(Text.AsSpan());
    }
}