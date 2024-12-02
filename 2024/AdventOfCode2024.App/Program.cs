using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day1RunA();

//BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunABenchmark>();

/*
RunGenerator.GeneratedRuns.Day2RunA();
RunGenerator.GeneratedRuns.Day2RunA2();
RunGenerator.GeneratedRuns.Day2RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day2RunBBenchmark>();
*/