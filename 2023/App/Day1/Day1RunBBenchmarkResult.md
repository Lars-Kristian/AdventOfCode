BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
[Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error   | StdDev  | Gen0    | Gen1   | Allocated |
|--------------------- |---------:|--------:|--------:|--------:|-------:|----------:|
| LogicOnly            | 494.1 us | 1.42 us | 1.26 us |       - |      - |     977 B |
| LogicAndReadFromDisk | 589.9 us | 3.72 us | 3.48 us | 15.6250 | 1.9531 |  102089 B |
