// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

//RunGenerator.GeneratedRuns.Day1RunB();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunBBenchmark>();