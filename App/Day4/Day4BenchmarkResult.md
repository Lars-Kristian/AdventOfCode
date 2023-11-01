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



BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2 [AttachedDebugger]

Job=ShortRun  Toolchain=InProcessNoEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

| Method               | Mean      | Error      | StdDev    | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|-----------:|----------:|-------:|-------:|----------:|
| LogicOnly            | 41.595 us |  3.6561 us | 0.2004 us |      - |      - |         - |
| LogicAndReadFromDisk | 64.475 us | 26.3855 us | 1.4463 us | 1.2207 | 0.1221 |   65480 B |



BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| LogicOnly            | 18.500 us | 0.0355 us | 0.0314 us |      - |      - |         - |
| LogicAndReadFromDisk | 42.389 us | 0.4127 us | 0.3861 us | 1.2817 | 0.1221 |   65592 B |