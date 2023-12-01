using BenchmarkDotNet.Running;

Console.WriteLine("Application starting...");

//RunGenerator.GeneratedRuns.Day1RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunBBenchmark>();