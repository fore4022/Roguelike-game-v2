using System;
using UnityEngine;
using Object = UnityEngine.Object;
public static class Util
{
    private static MonoScript monoScript = null;

    //public const float triggerTime = 0.9425f;

    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
    public static MonoBehaviour GetMonoBehaviour()
    {
        if(monoScript == null)
        {
            GameObject go = new GameObject("@MonoScript");

            monoScript = go.AddComponent<MonoScript>();

            Object.DontDestroyOnLoad(go);
        }

        return monoScript;
    }
    public static void StopCoroutine(Coroutine coroutine)
    {
        GetMonoBehaviour().StopCoroutine(coroutine);
    }
    public static void ResizeArray<T>(ref T[] array, int limits)
    {
        if(array.Length != limits)
        {
            Array.Resize(ref array, limits);
        }
    }
}