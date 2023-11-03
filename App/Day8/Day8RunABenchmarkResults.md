BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3570/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
[Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            |  46.44 us | 0.186 us | 0.165 us |      - |      - |         - |
| LogicAndReadFromDisk | 110.98 us | 1.022 us | 0.906 us | 9.6436 | 1.0986 |   60672 B |


BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2 [AttachedDebugger]

Toolchain=InProcessNoEmitToolchain  

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 26.20 us | 0.183 us | 0.162 us |      - |      - |         - |
| LogicAndReadFromDisk | 49.51 us | 0.337 us | 0.299 us | 1.1597 | 0.1221 |   60752 B |