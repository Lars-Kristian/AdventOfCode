BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3570/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
[Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| LogicOnly            | 22.83 us | 0.288 us | 0.269 us | 0.0610 |      - |     504 B |
| LogicAndReadFromDisk | 86.02 us | 1.004 us | 0.939 us | 9.6436 | 0.9766 |   61192 B |