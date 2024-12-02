using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day1RunA();
RunGenerator.GeneratedRuns.Day1RunB();

//BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunABenchmark>();
//BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunBBenchmark>();


RunGenerator.GeneratedRuns.Day2RunA();
RunGenerator.GeneratedRuns.Day2RunA2();
RunGenerator.GeneratedRuns.Day2RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day2RunBBenchmark>();
