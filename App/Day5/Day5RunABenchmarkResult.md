BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3570/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
[Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 22.83 us | 0.288 us | 0.269 us | 0.0610 |      - |     504 B |
| LogicAndReadFromDisk | 86.02 us | 1.004 us | 0.939 us | 9.6436 | 0.9766 |   61192 B |


BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2 [AttachedDebugger]

Toolchain=InProcessNoEmitToolchain  

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 12.39 us | 0.037 us | 0.033 us |      - |      - |     504 B |
| LogicAndReadFromDisk | 34.06 us | 0.276 us | 0.258 us | 1.1597 | 0.1221 |   61072 B |