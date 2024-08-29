using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
public class Util : MonoBehaviour
{
    public static async Task<T> LoadToPath<T>(string path) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        T result = handle.Result;

        Addressables.Release(handle);

        return result;
    }
    public static async Task<IList<T>> LoadToLable<T>(string lable) where T : Object
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
    public static void IsInvisible(Collider2D targetCollider)
    {
        if(!targetCollider.gameObject.activeSelf)
        {
            return;
        }

        Collider2D target = targetCollider;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, target.bounds)) { Destroy(targetCollider.gameObject); }
    }
    public static MonoBehaviour GetMonoBehaviour()
    {
        GameObject go = GameObject.Find("@MonoScript");

        if(go == null)
        {
            go = new GameObject("@MonoScript");

            go.AddComponent<MonoScript>();
        }

        return go.GetComponent<MonoScript>();
    }
}