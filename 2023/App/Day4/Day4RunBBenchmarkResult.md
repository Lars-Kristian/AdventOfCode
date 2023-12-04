BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev    | Median    | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|----------:|----------:|--------:|-------:|----------:|
| LogicOnly            |  35.36 us | 0.113 us |  0.095 us |  35.37 us |       - |      - |         - |
| LogicAndReadFromDisk | 125.96 us | 3.623 us | 10.682 us | 119.72 us | 16.3574 | 2.6855 |  104012 B |


BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 13.68 us | 0.044 us | 0.039 us |      - |      - |         - |
| LogicAndReadFromDisk | 41.44 us | 0.266 us | 0.249 us | 2.0752 | 0.3052 |  104416 B |
