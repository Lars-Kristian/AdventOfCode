BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            |  80.62 us | 1.162 us | 1.087 us |      - |      - |         - |
| LogicAndReadFromDisk | 149.39 us | 0.950 us | 0.794 us | 9.5215 | 0.9766 |   60690 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 28.36 us | 0.201 us | 0.188 us |      - |      - |         - |
| LogicAndReadFromDisk | 51.75 us | 0.379 us | 0.355 us | 1.2207 | 0.1221 |   62688 B |