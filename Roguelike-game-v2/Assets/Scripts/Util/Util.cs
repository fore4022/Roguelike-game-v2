using System;
using UnityEngine;
public static class Util
{
    //public const float triggerTime = 0.9425f;

    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
    public static void ResizeArray<T>(ref T[] array, int limits)
    {
        if(array.Length != limits)
        {
            Array.Resize(ref array, limits);
        }
    }
}