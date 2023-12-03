BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            |  48.03 us | 0.915 us | 0.898 us |       - |      - |      80 B |
| LogicAndReadFromDisk | 128.63 us | 0.988 us | 0.876 us | 15.3809 | 2.1973 |   96539 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 14.52 us | 0.027 us | 0.024 us |      - |      - |      80 B |
| LogicAndReadFromDisk | 41.80 us | 0.270 us | 0.253 us | 1.8921 | 0.1831 |   96536 B |
