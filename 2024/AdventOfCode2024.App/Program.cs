using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day5RunA();
RunGenerator.GeneratedRuns.Day5RunA2();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day5RunABenchmark>();
BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day5RunA2Benchmark>();
