BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.3570 (21H2)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.304
[Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT
DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT


| Method |      Mean |     Error |    StdDev | Allocated |
|------- |----------:|----------:|----------:|----------:|
|  Naive | 20.784 us | 0.2845 us | 0.2522 us |         - |
|   Swap | 18.270 us | 0.1500 us | 0.1330 us |         - |
| Unroll | 14.065 us | 0.0767 us | 0.0640 us |         - |
|   Simd |  1.466 us | 0.0213 us | 0.0199 us |         - |
