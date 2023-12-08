BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            |  83.70 us | 0.273 us | 0.255 us |       - |      - |         - |
| LogicAndReadFromDisk | 137.93 us | 1.050 us | 0.982 us | 10.4980 | 0.9766 |   66266 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Median   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 42.87 us | 1.475 us | 4.348 us | 45.95 us |      - |      - |         - |
| LogicAndReadFromDisk | 74.76 us | 1.471 us | 1.964 us | 75.11 us | 1.3428 | 0.1221 |   67728 B |


