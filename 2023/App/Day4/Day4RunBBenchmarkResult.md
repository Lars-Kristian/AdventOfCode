BenchmarkDotNet v0.13.10, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean      | Error    | StdDev   | Median    | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|----------:|--------:|-------:|----------:|
| LogicOnly            |  51.49 us | 1.026 us | 2.648 us |  52.67 us |       - |      - |         - |
| LogicAndReadFromDisk | 127.91 us | 1.080 us | 1.010 us | 128.29 us | 16.3574 | 2.6855 |  104012 B |