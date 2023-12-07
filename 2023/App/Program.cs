using BenchmarkDotNet.Running;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day7RunA();
RunGenerator.GeneratedRuns.Day7RunB();
//RunGenerator.GeneratedRuns.Day7RunBuildMap();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day7RunBBenchmark>();