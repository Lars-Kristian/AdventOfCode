using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day3RunA();
RunGenerator.GeneratedRuns.Day3RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day3RunBBenchmark>();