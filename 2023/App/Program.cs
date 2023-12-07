using BenchmarkDotNet.Running;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day7RunA();
RunGenerator.GeneratedRuns.Day7RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day7RunABenchmark>();