using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day6RunA();
RunGenerator.GeneratedRuns.Day6RunA2();
RunGenerator.GeneratedRuns.Day6RunB();
RunGenerator.GeneratedRuns.Day6RunB2();

//371
//1379
//1397
//1602
//BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day6RunBBenchmark>();
BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day6RunB2Benchmark>();
