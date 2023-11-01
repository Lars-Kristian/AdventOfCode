```

BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host] : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2 [AttachedDebugger]

Toolchain=InProcessNoEmitToolchain  

```
| Method               | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|--------------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| LogicOnly            | 41.476 μs | 0.2982 μs | 0.2790 μs |      - |      - |         - |
| LogicAndReadFromDisk | 64.547 μs | 0.5518 μs | 0.5161 μs | 1.2207 | 0.1221 |   65480 B |
| LogicOnlyTest        |  4.301 μs | 0.0171 μs | 0.0160 μs |      - |      - |         - |
