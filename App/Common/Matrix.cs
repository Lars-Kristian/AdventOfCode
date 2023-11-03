using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

public class Matrix : IDisposable
{
    public int Width;
    public int Height;
    public byte[] Data;

    public static Matrix Create(int width, int height)
    {
        var result = new Matrix
        {
            Width = width,
            Height = height,
            Data = ArrayPool<byte>.Shared.Rent(width * height)
        };

        return result;
    }

    public static Matrix FromByteArray(int width, int height, Span<byte> data)
    {
        var result = new Matrix
        {
            Width = width,
            Height = height,
            Data = ArrayPool<byte>.Shared.Rent(width * height)
        };

        data.CopyTo(result.Data);

        return result;
    }

    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(Data, true);
    }

    public void Transpose()
    {
        var tmp = ArrayPool<byte>.Shared.Rent(Width * Height);
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                tmp[Width * y + x] = Data[Width * x + y];
            }
        }

        ArrayPool<byte>.Shared.Return(Data);
        Data = tmp;
    }

    public void TransposeSwap()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = y + 1; x < Width; x++)
            {
                // ReSharper disable once SwapViaDeconstruction
                var tmp = Data[Width * x + y];
                Data[Width * x + y] = Data[Width * y + x];
                Data[Width * y + x] = tmp;
            }
        }
    }

    public void TransposeUnroll()
    {
        var tmp = ArrayPool<byte>.Shared.Rent(Width * Height);
        var remainingX = Width % 4;
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width - remainingX; x += 4)
            {
                tmp[Width * y + x] = Data[Width * x + y];
                tmp[Width * y + x + 1] = Data[Width * (x + 1) + y];
                tmp[Width * y + x + 2] = Data[Width * (x + 2) + y];
                tmp[Width * y + x + 3] = Data[Width * (x + 3) + y];
            }
        }

        for (var y = 0; y < Height; y++)
        {
            for (var x = Width - remainingX; x < Width; x++)
            {
                tmp[Width * y + x] = Data[Width * x + y];
            }
        }

        ArrayPool<byte>.Shared.Return(Data);
        Data = tmp;
    }

    public void TransposeSimd()
    {
        var tmp = ArrayPool<byte>.Shared.Rent(Width * Height);
        const int size = 8;
        var remainingX = Width % size;
        var remainingY = Height % size;
        for (var y = 0; y < Height - remainingY; y += size)
        {
            for (var x = 0; x < Width - remainingX; x += size)
            {
                TransposeTest.Transpose3(Data, Width * y + x, tmp, Width * x + y, Width);
            }
        }

        for (var y = 0; y < Height - remainingY; y++)
        {
            for (var x = Width - remainingX; x < Width; x++)
            {
                tmp[Width * y + x] = Data[Width * x + y];
            }
        }

        for (var y = Height - remainingY; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                tmp[Width * y + x] = Data[Width * x + y];
            }
        }

        for (var y = Height - remainingY; y < Height; y++)
        {
            for (var x = Width - remainingX; x < Width; x++)
            {
                tmp[Width * y + x] = Data[Width * x + y];
            }
        }

        ArrayPool<byte>.Shared.Return(Data);
        Data = tmp;
    }
}

public static class TransposeTest
{
    public static void Transpose(byte[] data)
    {
        if (data.Length != 64) return;

        unsafe
        {
            fixed (byte* dataPtr = &data[0])
            {
                var tmp = Sse2.LoadVector128(dataPtr).AsByte();
                var row1 = Sse2.UnpackLow(tmp, Vector128<byte>.Zero).AsInt16();
                var row2 = Sse2.UnpackHigh(tmp, Vector128<byte>.Zero).AsInt16();
                tmp = Sse2.LoadVector128(dataPtr + 16).AsByte();
                var row3 = Sse2.UnpackLow(tmp, Vector128<byte>.Zero).AsInt16();
                var row4 = Sse2.UnpackHigh(tmp, Vector128<byte>.Zero).AsInt16();
                tmp = Sse2.LoadVector128(dataPtr + 32).AsByte();
                var row5 = Sse2.UnpackLow(tmp, Vector128<byte>.Zero).AsInt16();
                var row6 = Sse2.UnpackHigh(tmp, Vector128<byte>.Zero).AsInt16();
                tmp = Sse2.LoadVector128(dataPtr + 48).AsByte();
                var row7 = Sse2.UnpackLow(tmp, Vector128<byte>.Zero).AsInt16();
                var row8 = Sse2.UnpackHigh(tmp, Vector128<byte>.Zero).AsInt16();

                var vTemp1 = Sse2.UnpackLow(row1, row2);
                var vTemp2 = Sse2.UnpackLow(row3, row4);
                var vTemp3 = Sse2.UnpackLow(row5, row6);
                var vTemp4 = Sse2.UnpackLow(row7, row8);

                var tt1 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32());
                var tt2 = Sse2.UnpackLow(vTemp3.AsInt32(), vTemp4.AsInt32());

                var result1 = Sse2.UnpackLow(tt1.AsInt64(), tt2.AsInt64()).AsInt16();
                var result2 = Sse2.UnpackHigh(tt1.AsInt64(), tt2.AsInt64()).AsInt16();

                tt1 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32());
                tt2 = Sse2.UnpackHigh(vTemp3.AsInt32(), vTemp4.AsInt32());

                var result3 = Sse2.UnpackLow(tt1.AsInt64(), tt2.AsInt64()).AsInt16();
                var result4 = Sse2.UnpackHigh(tt1.AsInt64(), tt2.AsInt64()).AsInt16();

                vTemp1 = Sse2.UnpackHigh(row1, row2);
                vTemp2 = Sse2.UnpackHigh(row3, row4);
                vTemp3 = Sse2.UnpackHigh(row5, row6);
                vTemp4 = Sse2.UnpackHigh(row7, row8);

                tt1 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32());
                tt2 = Sse2.UnpackLow(vTemp3.AsInt32(), vTemp4.AsInt32());

                var result5 = Sse2.UnpackLow(tt1.AsInt64(), tt2.AsInt64()).AsInt16();
                var result6 = Sse2.UnpackHigh(tt1.AsInt64(), tt2.AsInt64()).AsInt16();

                tt1 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32());
                tt2 = Sse2.UnpackHigh(vTemp3.AsInt32(), vTemp4.AsInt32());

                var result7 = Sse2.UnpackLow(tt1.AsInt64(), tt2.AsInt64()).AsInt16();
                var result8 = Sse2.UnpackHigh(tt1.AsInt64(), tt2.AsInt64()).AsInt16();

                var shuffleMask = Vector128.Create(0, 2, 4, 6, 8, 10, 12, 14, sbyte.MaxValue, sbyte.MaxValue,
                    sbyte.MaxValue,
                    sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue);

                result1 = Ssse3.Shuffle(result1.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((float*)dataPtr, result1.AsSingle());
                result2 = Ssse3.Shuffle(result2.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 1, result2.AsDouble());
                result3 = Ssse3.Shuffle(result3.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 2, result3.AsDouble());
                result4 = Ssse3.Shuffle(result4.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 3, result4.AsDouble());
                result5 = Ssse3.Shuffle(result5.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 4, result5.AsDouble());
                result6 = Ssse3.Shuffle(result6.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 5, result6.AsDouble());
                result7 = Ssse3.Shuffle(result7.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 6, result7.AsDouble());
                result8 = Ssse3.Shuffle(result8.AsSByte(), shuffleMask).AsInt16();
                Sse2.StoreLow((double*)dataPtr + 7, result8.AsDouble());
            }
        }
    }

    public static void Transpose2(byte[] data)
    {
        if (data.Length != 64) return;

        unsafe
        {
            var shuffleMask = Vector128.Create((byte)0, 8, 1, 9, 2, 10, 3, 11, 4, 12, 5, 13, 6, 14, 7, 15);

            var dataPtr2 = (double*)Unsafe.AsPointer(ref data[0]);
            var dataPtr3 = Unsafe.AsPointer(ref data);

            var tmp = Sse2.LoadLow(Vector128<double>.Zero, dataPtr2);
            tmp = Sse2.LoadHigh(tmp, dataPtr2 + 1);

            var row12 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, dataPtr2 + 2);
            tmp = Sse2.LoadHigh(tmp, dataPtr2 + 3);

            var row34 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, dataPtr2 + 4);
            tmp = Sse2.LoadHigh(tmp, dataPtr2 + 5);

            var row56 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, dataPtr2 + 6);
            tmp = Sse2.LoadHigh(tmp, dataPtr2 + 7);

            var row78 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            var vTemp1 = Sse2.UnpackLow(row12.AsInt16(), row34.AsInt16());
            var vTemp2 = Sse2.UnpackLow(row56.AsInt16(), row78.AsInt16());

            var result12 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();
            var result34 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();

            vTemp1 = Sse2.UnpackHigh(row12.AsInt16(), row34.AsInt16());
            vTemp2 = Sse2.UnpackHigh(row56.AsInt16(), row78.AsInt16());

            var result56 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();
            var result78 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();

            Ssse3.StoreLow(dataPtr2, result12.AsDouble());
            Ssse3.StoreHigh(dataPtr2 + 1, result12.AsDouble());
            Ssse3.StoreLow(dataPtr2 + 2, result34.AsDouble());
            Ssse3.StoreHigh(dataPtr2 + 3, result34.AsDouble());
            Ssse3.StoreLow(dataPtr2 + 4, result56.AsDouble());
            Ssse3.StoreHigh(dataPtr2 + 5, result56.AsDouble());
            Ssse3.StoreLow(dataPtr2 + 6, result78.AsDouble());
            Ssse3.StoreHigh(dataPtr2 + 7, result78.AsDouble());
        }
    }

    public static void Transpose3(byte[] source, int sourceIndex, byte[] destination, int destinationIndex, int stride)
    {
        unsafe
        {
            var shuffleMask = Vector128.Create((byte)0, 8, 1, 9, 2, 10, 3, 11, 4, 12, 5, 13, 6, 14, 7, 15);

            var dataPtr = (byte*)Unsafe.AsPointer(ref source[sourceIndex]);
            var destinationPtr = (byte*)Unsafe.AsPointer(ref destination[destinationIndex]);

            var tmp = Sse2.LoadLow(Vector128<double>.Zero, (double*)dataPtr);
            tmp = Sse2.LoadHigh(tmp, (double*)(dataPtr + stride));

            var row12 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, (double*)(dataPtr + stride * 2));
            tmp = Sse2.LoadHigh(tmp, (double*)(dataPtr + stride * 3));

            var row34 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, (double*)(dataPtr + stride * 4));
            tmp = Sse2.LoadHigh(tmp, (double*)(dataPtr + stride * 5));

            var row56 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            tmp = Sse2.LoadLow(Vector128<double>.Zero, (double*)(dataPtr + stride * 6));
            tmp = Sse2.LoadHigh(tmp, (double*)(dataPtr + stride * 7));

            var row78 = Ssse3.Shuffle(tmp.AsByte(), shuffleMask);

            var vTemp1 = Sse2.UnpackLow(row12.AsInt16(), row34.AsInt16());
            var vTemp2 = Sse2.UnpackLow(row56.AsInt16(), row78.AsInt16());

            var result12 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();
            var result34 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();

            vTemp1 = Sse2.UnpackHigh(row12.AsInt16(), row34.AsInt16());
            vTemp2 = Sse2.UnpackHigh(row56.AsInt16(), row78.AsInt16());

            var result56 = Sse2.UnpackLow(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();
            var result78 = Sse2.UnpackHigh(vTemp1.AsInt32(), vTemp2.AsInt32()).AsByte();

            Sse2.StoreLow((double*)destinationPtr, result12.AsDouble());
            Sse2.StoreHigh((double*)(destinationPtr + stride * 1), result12.AsDouble());
            Sse2.StoreLow((double*)(destinationPtr + stride * 2), result34.AsDouble());
            Sse2.StoreHigh((double*)(destinationPtr + stride * 3), result34.AsDouble());
            Sse2.StoreLow((double*)(destinationPtr + stride * 4), result56.AsDouble());
            Sse2.StoreHigh((double*)(destinationPtr + stride * 5), result56.AsDouble());
            Sse2.StoreLow((double*)(destinationPtr + stride * 6), result78.AsDouble());
            Sse2.StoreHigh((double*)(destinationPtr + stride * 7), result78.AsDouble());
        }
    }
}