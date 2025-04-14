using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
public class Util
{
    public static List<GameObject> resourceList = new();

    private static MonoScript monoScript = null;

    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
    public static void LoadingScene(string path)
    {
        Addressables.LoadSceneAsync(path, UnityEngine.SceneManagement.LoadSceneMode.Single).WaitForCompletion();
    }
    public static T LoadingToPath<T>(string path) where T : Object
    {
        T resources = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
        T result = resources;

        if(typeof(T) == typeof(GameObject))
        {
            resourceList.Add(resources as GameObject);
        }
        else
        {
            Addressables.Release(resources);
        }

        return result;
    }
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
    public static Transform GetChildren(Transform transform, int index)
    {
        if(index > transform.childCount)
        {
            return null;
        }

        return transform.GetChild(index);
    }
    public static List<T> GetComponentsInChildren<T>(Transform transform) where T : Component
    {
        List<T> components = new();

        for(int index = 0; index < transform.childCount; index++)
        {
            if(transform.GetChild(index).TryGetComponent(out T component))
            {
                components.Add(component);
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
            for (int index = 0; index < transform.childCount; index++)
            {
                if (transform.GetChild(index).TryGetComponent(out T component))
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