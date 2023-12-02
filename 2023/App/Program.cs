using BenchmarkDotNet.Running;
//using Generator;

Console.WriteLine("Application starting...");

//RunGenerator.GeneratedRuns.Day2RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day2RunBBenchmark>();