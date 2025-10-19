using System;
public static class ArrayUtil
{
    public static void ResizeArray<T>(ref T[] array, int limits)
    {
        if (array.Length != limits)
        {
            Array.Resize(ref array, limits);
        }
    }
}