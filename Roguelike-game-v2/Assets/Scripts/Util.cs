using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
public class Util
{
    //public static T LoadToPath<T>(string path) where T : Object
    //{
    //    T result = null;

    //    Task waitForLoad = Task.Run(async() =>
    //    {
    //        result = await LoadingToPath<T>(path);
    //    });

    //    waitForLoad.Wait();

    //    return result;
    //}
    public static async Task<T> LoadToPath<T>(string path)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        T result = handle.Result;

        Addressables.Release(handle);

        return result;
    }
    public static async Task<List<T>> LoadToLable<T>(string lable) where T : Object
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(lable, null);

        await handle.Task;

        List<T> resultList = new List<T>();                                                                                 

        foreach(T result in handle.Result)
        {
            resultList.Add(result);
        }

        Addressables.Release(handle);

        return resultList;
    }
    public static async void InitAddressableAsset()
    {
        await Addressables.InitializeAsync().Task;
    }
}