using BenchmarkDotNet.Running;
using Generator;

Console.WriteLine("Application has started...");

RunGenerator.GeneratedRuns.Day3RunA();
RunGenerator.GeneratedRuns.Day3RunB();
RunGenerator.GeneratedRuns.Day3RunB2();
//RunGenerator.GeneratedRuns.Day3RunA2();
//RunGenerator.GeneratedRuns.Day3RunB();

//BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day3RunB2Benchmark>();
