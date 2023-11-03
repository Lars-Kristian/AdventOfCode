using App.Day5;
using App.Day6;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkGenerator;
using RunGenerator;


Console.WriteLine("Application is running...");

//GeneratedRuns.Day5RunB();
BenchmarkRunner.Run<Day5RunBBenchmark>();