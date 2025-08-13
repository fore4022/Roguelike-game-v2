using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
public static class Util
{
    public static List<AsyncOperationHandle> handleList = new();
    public static Type type_GameObject = typeof(GameObject);
    public static Type type_ScriptableObject = typeof(ScriptableObject);
    public static Type type_Sprite = typeof(Sprite);

    private static MonoScript monoScript = null;

    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
    public static void LoadingScene(string path)
    {
        Addressables.LoadSceneAsync(path, UnityEngine.SceneManagement.LoadSceneMode.Single).WaitForCompletion();
    }
    public static T LoadingToPath<T>(string path, bool releasable = true) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);
        T result = handle.WaitForCompletion();

        if(releasable)
        {
            Type type = typeof(T);

            if(type == type_GameObject || type == type_ScriptableObject || type == type_Sprite)
            {
                handleList.Add(handle);
            }
            else
            {
                Addressables.Release(handle);
            }
        }

        return result;
    }
    public static void AddressableResourcesRelease()
    {
        foreach(AsyncOperationHandle handle in handleList)
        {
            Addressables.Release(handle);
        }

        handleList.Clear();
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