using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Object = UnityEngine.Object;
public class Util
{
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
    public static async Task<IList<T>> LoadingToLable<T>(string lable) where T : Object
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(lable, null);

        await handle.Task;

        IList<T> resultList = handle.Result;

        Addressables.Release(handle);

        return resultList;
    }
    public static async void InitAddressableAsset()
    {
        await Addressables.InitializeAsync().Task;
    }
    public static MonoBehaviour GetMonoBehaviour()
    {
        GameObject go = GameObject.Find("@MonoScript");

        if(go == null)
        {
            go = new GameObject("@MonoScript");

            go.AddComponent<MonoScript>();

            Object.DontDestroyOnLoad(go);
        }

        return go.GetComponent<MonoScript>();
    }
    public static List<T> GetComponentsInChildren<T>(Transform transform) where T : Component
    {
        List<T> components = new();

        for(int index = 0; index < transform.childCount; index++)
        {
            if(transform.GetChild(index).TryGetComponent<T>(out T component))
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
                if (transform.GetChild(index).TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }
        }

        return default(T);
    }
}