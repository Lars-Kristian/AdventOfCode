namespace App.Common;

public static class LcmUtil
{
    /// Yoiked from https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
    public static long Lcm(Span<long> numbers)
    {
        if (numbers.Length < 2)
            throw new ArgumentException("you must pass two or more numbers");
        return Lcm(numbers, 0);
    }
    
    /*
    public static long Lcm2(Span<long> numbers)
    {
        if (numbers.Length == 1)
            return numbers[0];
        
        Span<long> tmpResult = stackalloc long[numbers.Length / 2];
        for (var index = 0; index < tmpResult.Length; index++)
        {
            tmpResult[index] = Lcm(numbers[index * 2], numbers[index * 2 + 1]);
        }
        if (numbers.Length % 2 == 1)
        
        
        
        return Lcm(numbers, 0);
    }
    */

    private static long Lcm(Span<long> numbers, int i)
    {
        if (i + 2 == numbers.Length)
        {
            return Lcm(numbers[i], numbers[i + 1]);
        }
        else
        {
            return Lcm(numbers[i], Lcm(numbers, i + 1));
        }
    }

    public static long Lcm(long a, long b)
    {
        return (a * b / Gcm(a, b));
    }

    private static long Gcm(long a, long b)
    {
        long tmp;
        while (b != 0)
        {
            tmp = b;
            b = a % b;
            a = tmp;
        }

        return a;
    }
}