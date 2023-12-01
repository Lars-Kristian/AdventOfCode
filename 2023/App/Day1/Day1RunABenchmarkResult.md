BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            | 20.65 us | 0.058 us | 0.049 us |       - |      - |         - |
| LogicAndReadFromDisk | 89.78 us | 0.630 us | 0.558 us | 15.9912 | 2.5635 |  101123 B |


BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 13.92 us | 0.064 us | 0.053 us |      - |      - |         - |
| LogicAndReadFromDisk | 38.36 us | 0.438 us | 0.410 us | 2.0142 | 0.3052 |  103120 B |