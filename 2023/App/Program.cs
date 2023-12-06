using BenchmarkDotNet.Running;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day6RunA();
RunGenerator.GeneratedRuns.Day6RunB();
BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day6RunBBenchmark>();