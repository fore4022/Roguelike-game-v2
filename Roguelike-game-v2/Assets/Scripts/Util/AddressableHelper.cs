using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
/// <summary>
/// <para>
/// Addressables ����� ���� �� �� �ε������� ����� ����
/// </para>
/// �񵿱� �ε��� �޸� ���� �� ����ȭ
/// </summary>
public static class AddressableHelper
{
    public static List<AsyncOperationHandle> handleList = new();
    public static Type type_GameObject = typeof(GameObject);
    public static Type type_ScriptableObject = typeof(ScriptableObject);
    public static Type type_Sprite = typeof(Sprite);

    // �ּҸ� ���ؼ� �� �ε�
    public static void LoadingScene(string path)
    {
        Addressables.LoadSceneAsync(path, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    // �ּҸ� ���ؼ� ������ �񵿱� �ε�, �ڵ��� ���� ���θ� ���� ����
    public static async Task<T> LoadingToPath<T>(string path, bool releasable = true) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        T result = handle.Result;

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
    // handleList�� ��ϵ� ��� handle ����
    public static void ResourcesRelease()
    {
        foreach(AsyncOperationHandle handle in handleList)
        {
            Addressables.Release(handle);
        }

        handleList.Clear();
    }
}