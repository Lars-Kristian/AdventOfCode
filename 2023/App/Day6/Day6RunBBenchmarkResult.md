BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean        | Error     | StdDev    | Gen0   | Allocated |
|--------------------- |------------:|----------:|----------:|-------:|----------:|
| LogicOnly            |    101.6 ns |   2.00 ns |   2.05 ns |      - |         - |
| LogicAndReadFromDisk | 39,797.8 ns | 168.87 ns | 131.84 ns | 1.3428 |    8536 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean         | Error     | StdDev    | Gen0   | Allocated |
|--------------------- |-------------:|----------:|----------:|-------:|----------:|
| LogicOnly            |     59.57 ns |  0.434 ns |  0.406 ns |      - |         - |
| LogicAndReadFromDisk | 13,584.83 ns | 75.699 ns | 67.105 ns | 0.1678 |    8536 B |
