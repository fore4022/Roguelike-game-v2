using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using UnityEngine;
public class Util : MonoBehaviour
{
    public static async Task<T> LoadToPath<T>(string path) where T : Object
    {
        Debug.Log("zxcv");
        T handle = await AsyncLoading<T>(path);

        return handle;
    }
    public static async Task<T> AsyncLoading<T>(string path)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        try
        {
            Debug.Log("asdf");
            await handle.Task;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Exception during asset loading: {ex.Message}");
            return handle.Result;
        }

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load asset at path: {path}. Status: {handle.Status}");
            return handle.Result;
        }

        return handle.Result;
    }
}