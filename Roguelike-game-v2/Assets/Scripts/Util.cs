using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
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

        return handle.Result;
    }
}