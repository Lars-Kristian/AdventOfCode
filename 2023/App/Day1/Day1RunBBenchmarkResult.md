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
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Median    | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|----------:|-------:|-------:|----------:|
| LogicOnly            |  64.94 us | 0.205 us | 0.348 us |  64.77 us |      - |      - |         - |
| LogicAndReadFromDisk | 105.59 us | 0.740 us | 0.693 us | 105.49 us | 1.9531 | 0.2441 |  103121 B |



BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Median   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 66.96 us | 1.497 us | 4.414 us | 63.63 us |      - |      - |         - |
| LogicAndReadFromDisk | 94.87 us | 2.568 us | 7.571 us | 89.05 us | 1.9531 | 0.2441 |  103121 B |