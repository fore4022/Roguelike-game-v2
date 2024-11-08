using System;
using System.Diagnostics;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class Scene_Manager
{
    public AsyncOperationHandle<SceneInstance>? sceneHandle;

    public Action loadScene = null;

    private string currentScene;
    private string path = null;

    public string CurrentScene { get { return currentScene; } }
    public void LoadScene(string path)
    {
        this.path = path;

        Managers.UI.ClearDictionary();

        Managers.UI.ShowUI<SceneLoading_UI>();

        if(sceneHandle != null)
        {
            loadScene.Invoke();

            Addressables.Release(sceneHandle);
        }
    }
    public async void SetScene()
    {
        if(path == null)
        {
            return;
        }

        sceneHandle = await Util.LoadingScene(path);

        currentScene = path;

        path = null;
    }
}
