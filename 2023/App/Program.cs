// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

//RunGenerator.GeneratedRuns.Day1RunB2();

BenchmarkRunner.Run<BenchmarkGenerator.GeneratedBenchmarks.Day1RunB2Benchmark>();