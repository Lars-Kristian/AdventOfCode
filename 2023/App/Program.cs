using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application starting...");

//RunGenerator.GeneratedRuns.Day3RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day3RunABenchmark>();