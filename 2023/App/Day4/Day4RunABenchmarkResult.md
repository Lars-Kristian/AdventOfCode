BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            |  44.21 us | 0.134 us | 0.119 us |       - |      - |         - |
| LogicAndReadFromDisk | 125.54 us | 1.891 us | 1.677 us | 16.3574 | 2.6855 |  104012 B |