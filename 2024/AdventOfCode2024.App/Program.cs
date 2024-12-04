using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");


RunGenerator.GeneratedRuns.Day4RunB();
RunGenerator.GeneratedRuns.Day4RunB2();
RunGenerator.GeneratedRuns.Day4RunB3();
RunGenerator.GeneratedRuns.Day4RunB4();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day4RunB4Benchmark>();
