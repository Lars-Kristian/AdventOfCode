using BenchmarkDotNet.Attributes;
using ConsoleApp.Common;

namespace ConsoleApp.Missions;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class Day6Benchmark
{
    public Day6Benchmark()
    {
        Text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Missions/Day6.input"));
    }
    
    private string Text { get; }
    
    [Benchmark]
    public void LogicOnly()
    {
        Day6.RunB(Text.AsSpan());
    }
    
    [Benchmark]
    public void OtherLogicOnly()
    {
        Day6.RunOther(Text);
    }
    
    /*
    [Benchmark]
    public void LogicAndReadFileAndWriteFile()
    {
        Day6.RunB(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Missions/Day6.input")));
    }
    */
}