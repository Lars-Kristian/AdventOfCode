BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean        | Error     | StdDev    | Gen0   | Allocated |
|--------------------- |------------:|----------:|----------:|-------:|----------:|
| LogicOnly            |    148.3 ns |   1.18 ns |   1.11 ns |      - |         - |
| LogicAndReadFromDisk | 39,258.3 ns | 725.18 ns | 712.22 ns | 1.3428 |    8536 B |



BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean        | Error    | StdDev   | Gen0   | Allocated |
|--------------------- |------------:|---------:|---------:|-------:|----------:|
| LogicOnly            |    115.4 ns |  0.29 ns |  0.26 ns |      - |         - |
| LogicAndReadFromDisk | 13,772.2 ns | 64.35 ns | 60.19 ns | 0.1678 |    8536 B |

