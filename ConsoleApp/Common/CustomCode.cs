using System.Runtime.CompilerServices;

namespace ConsoleApp.Common;

public static class CustomCode
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReadFileFast(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        return streamReader.ReadToEnd();
    }
}