BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3570/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
[Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Job=ShortRun  Toolchain=InProcessNoEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3

| Method               | Mean      | Error      | StdDev    | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|-----------:|----------:|--------:|-------:|----------:|
| LogicOnly            | 27.758 us |  4.6847 us | 0.2568 us |       - |      - |         - |
| LogicAndReadFromDisk | 92.830 us | 11.4065 us | 0.6252 us | 10.1318 | 1.2207 |   63736 B |