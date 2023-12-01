BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
[Host] : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain

| Method               | Mean     | Error   | StdDev  | Gen0    | Gen1   | Allocated |
|--------------------- |---------:|--------:|--------:|--------:|-------:|----------:|
| LogicOnly            | 317.7 us | 0.73 us | 0.61 us |       - |      - |         - |
| LogicAndReadFromDisk | 409.8 us | 3.18 us | 2.97 us | 15.6250 | 2.4414 |  101124 B |