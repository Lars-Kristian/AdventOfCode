BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error   | StdDev  | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|--------:|--------:|-------:|-------:|----------:|
| LogicOnly            | 134.8 us | 0.72 us | 0.67 us |      - |      - |         - |
| LogicAndReadFromDisk | 206.1 us | 0.94 us | 0.83 us | 9.5215 | 0.9766 |   60690 B |