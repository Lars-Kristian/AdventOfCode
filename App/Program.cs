using BenchmarkDotNet.Running;
using BenchmarkGenerator;
using RunGenerator;


Console.WriteLine("Application is running...");

GeneratedRuns.Day8RunB2();
BenchmarkRunner.Run<GeneratedBenchmarks.Day8RunBBenchmark>();