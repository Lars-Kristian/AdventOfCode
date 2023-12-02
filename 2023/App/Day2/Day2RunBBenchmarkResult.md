BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Median   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 24.90 us | 0.270 us | 0.211 us | 24.80 us |      - |      - |         - |
| LogicAndReadFromDisk | 95.05 us | 2.180 us | 6.393 us | 93.06 us | 9.7656 | 1.0986 |   61850 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 21.08 us | 0.026 us | 0.022 us |      - |      - |         - |
| LogicAndReadFromDisk | 42.08 us | 0.207 us | 0.173 us | 1.2207 | 0.0610 |   61648 B |