using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Object = UnityEngine.Object;
public class Util
{
    private static MonoScript monoScript = null;

    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
    public static async Task<AsyncOperationHandle<SceneInstance>> LoadingScene(string path)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(path);

        await handle.Task;

        return handle;
    }
    public static async Task<T> LoadingToPath<T>(string path) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        T result = handle.Result;

        Addressables.Release(handle);

        return result;
    }
    public static async void InitAddressableAsset()
    {
        await Addressables.InitializeAsync().Task;
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
}