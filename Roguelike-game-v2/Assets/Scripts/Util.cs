using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
public class Util
{
    public static T LoadAddressableAsset<T>(string path) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        Task.Run(() => AsynchronousLoading(handle));

        return handle.Result;
    }
    public static T LoadAddressableAssets<T>(string path) where T : Object
    {


        return null;
    }
    [Obsolete]
    public static async Task AsynchronousLoading<T>(AsyncOperationHandle<T> handle)
    {
        while(handle.PercentComplete < 1)
        {
            if(handle.Status == AsyncOperationStatus.Failed) { CancellationToken; }

            await Task.Delay(1);
        }
    }
}