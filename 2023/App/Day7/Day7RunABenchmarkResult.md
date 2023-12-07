BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error   | StdDev  | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|--------:|--------:|-------:|-------:|----------:|
| LogicOnly            | 123.2 us | 1.10 us | 0.98 us |      - |      - |         - |
| LogicAndReadFromDisk | 195.1 us | 1.09 us | 0.91 us | 9.5215 | 0.9766 |   60690 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 29.00 us | 0.104 us | 0.098 us |      - |      - |         - |
| LogicAndReadFromDisk | 51.55 us | 0.518 us | 0.484 us | 1.2207 | 0.1221 |   62688 B |


