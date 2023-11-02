using App.Day5;
using App.Day6;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkGenerator;
using RunGenerator;


Console.WriteLine("Application is running...");

//GeneratedRuns.Day5RunA();

//GeneratedBenchmarks.Day4RunB();

BenchmarkRunner.Run<Day5Benchmark>();


//var summaries = BenchmarkSwitcher.FromTypes(new[] { typeof(Day6Benchmark) }).Run(null, new DebugInProcessConfig());
/*
static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
Main(new string[0]);
*/

//var debug = "";