using BenchmarkDotNet.Running;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day8RunA();
RunGenerator.GeneratedRuns.Day8RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day8RunBBenchmark>();