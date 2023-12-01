BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19044.3693/21H2/November2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.304
  [Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Toolchain=InProcessNoEmitToolchain  

| Method               | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| LogicOnly            |  55.15 us | 0.649 us | 0.576 us |       - |      - |         - |
| LogicAndReadFromDisk | 135.21 us | 0.681 us | 0.637 us | 15.8691 | 2.4414 |  101112 B |