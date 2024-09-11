using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class Scene_Manager
{
    public AsyncOperationHandle<SceneInstance>? sceneHandle;

    public Action loadScene = null;

    public string scenePath;

    public async void LoadScene(string path)
    {
        if(sceneHandle != null)
        {
            loadScene.Invoke();

            Addressables.Release(sceneHandle);
        }

        sceneHandle = await Util.LoadingScene(path);

        scenePath = path;
    }
}
