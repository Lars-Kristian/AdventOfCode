using BenchmarkDotNet.Running;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day5RunA();
RunGenerator.GeneratedRuns.Day5RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day5RunABenchmark>();