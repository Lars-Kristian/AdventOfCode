BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            | 18.07 us | 0.523 us | 1.542 us |       - |      - |         - |
| LogicAndReadFromDisk | 97.20 us | 0.943 us | 0.836 us | 15.3809 | 2.1973 |   96459 B |