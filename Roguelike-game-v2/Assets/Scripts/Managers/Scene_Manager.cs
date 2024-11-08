using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class Scene_Manager
{
    public AsyncOperationHandle<SceneInstance>? sceneHandle;

    public Action loadScene = null;

    private string currentScene;

    public string CurrentScene { get { return currentScene; } }
    public async void LoadScene(string path)
    {
        Managers.UI.ClearDictionary();

        Managers.UI.ShowUI<SceneLoading_UI>();

        if(sceneHandle != null)
        {
            loadScene.Invoke();

            Addressables.Release(sceneHandle);
        }

        //await Task.Delay(() => Managers.UI.GetUI<SceneLoading_UI>().PlayerAnimation != null ? 0 :1);

        sceneHandle = await Util.LoadingScene(path);

        currentScene = path;
    }
}
