using App.Day5;
using App.Day6;
using App.Day8;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkGenerator;
using RunGenerator;


Console.WriteLine("Application is running...");

GeneratedRuns.Day8RunA();
//BenchmarkRunner.Run<Day8RunBBenchmark>();