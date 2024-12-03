using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day3RunA();
RunGenerator.GeneratedRuns.Day3RunB();
RunGenerator.GeneratedRuns.Day3RunB2();
RunGenerator.GeneratedRuns.Day3RunB3();
RunGenerator.GeneratedRuns.Day3RunB4();
//RunGenerator.GeneratedRuns.Day3RunA2();
//RunGenerator.GeneratedRuns.Day3RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day3RunB4Benchmark>();
