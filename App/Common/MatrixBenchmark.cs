using BenchmarkDotNet.Attributes;


[MemoryDiagnoser()]
public class MatrixBenchmark
{
    private class TestData
    {
        public static byte[] CreateByteArray(int width, int height)
        {
            var result = new byte[width * height];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(i % 0xff);
            }

            return result;
        }
        
        public static byte[] CreateByteArrayTransposed(int width, int height)
        {
            var result = new byte[width * height];
            for (var i = 0; i < result.Length; i++)
            {
                var x = i % width;
                var y = i / height;
                result[width * x + y] = (byte)(i % 0xff);
            }

            return result;
        }
    }
    
    Matrix matrix = Matrix.FromByteArray(128, 128, TestData.CreateByteArray(128, 128));
    
    [Benchmark]
    public void Naive()
    {
        matrix.Transpose();
    }
    
    [Benchmark]
    public void Swap()
    {
        matrix.TransposeSwap();
    }
    
    [Benchmark]
    public void Unroll()
    {
        matrix.TransposeUnroll();
    }
    
    [Benchmark]
    public void Simd()
    {
        matrix.TransposeSimd();
    }
    
    /*
    [Benchmark]
    public void D()
    {
        TransposeTest.Transpose(matrix.Data);
    }
    */
}