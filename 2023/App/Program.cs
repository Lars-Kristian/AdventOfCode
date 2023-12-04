using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day4RunA();
RunGenerator.GeneratedRuns.Day4RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day4RunABenchmark>();