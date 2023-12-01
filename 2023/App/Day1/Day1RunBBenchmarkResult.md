BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error   | StdDev  | Gen0    | Gen1   | Allocated |
|--------------------- |---------:|--------:|--------:|--------:|-------:|----------:|
| LogicOnly            | 166.4 us | 0.67 us | 0.56 us |       - |      - |         - |
| LogicAndReadFromDisk | 239.4 us | 1.47 us | 1.37 us | 15.6250 | 2.4414 |  101124 B |


BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Median   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 62.57 us | 0.400 us | 0.936 us | 62.32 us |      - |      - |         - |
| LogicAndReadFromDisk | 95.06 us | 1.895 us | 4.078 us | 92.89 us | 1.9531 | 0.2441 |  103121 B |