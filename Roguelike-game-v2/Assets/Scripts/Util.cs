using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;

public class Util
{
    public static async Task<T> LoadToPath<T>(string path) where T : Object
    {
        T handle = await AsyncLoading<T>(path);

        return handle;
    }
    public static async Task<T> AsyncLoading<T>(string path)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        if (handle.Result == null)
        {
            Debug.Log("asdf");
            return handle.Result;
        }

        return handle.Result;
    }
}