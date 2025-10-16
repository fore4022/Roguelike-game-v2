using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public static class Util
{
    private static MonoScript monoScript = null;

    public const float triggerTime = 0.9425f;

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
    public static Transform GetChildren(Transform transform, int index)
    {
        if(index > transform.childCount)
        {
            return null;
        }

        return transform.GetChild(index);
    }
    public static List<T> GetComponentsInChildren<T>(Transform transform, bool recursive = false) where T : Component
    {
        List<T> components = new();

        if(recursive)
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                components.AddRange(GetComponentsInChildren<T>(transform.GetChild(index), true));
            }

            if(transform.TryGetComponent(out T component))
            {
                components.Add(component);
            }
        }
        else
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                if(transform.GetChild(index).TryGetComponent(out T component))
                {
                    components.Add(component);
                }
            }
        }

        return components;
    }
    public static T GetComponentInChildren<T>(Transform transform, bool recursive = false) where T : Component
    {
        if(recursive)
        {
            return transform.GetComponentInChildren<T>();
        }
        else
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                if(transform.GetChild(index).TryGetComponent(out T component))
                {
                    return component;
                }
            }
        }

        return default;
    }
    public static void ResizeArray<T>(ref T[] array, int limits)
    {
        if(array.Length != limits)
        {
            Array.Resize(ref array, limits);
        }
    }
}