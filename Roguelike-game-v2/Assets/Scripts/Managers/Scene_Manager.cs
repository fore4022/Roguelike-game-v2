using System;
using System.Collections;
using UnityEngine;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene = null;
    private string sceneName;
    private bool isLoad = false;

    public bool isSceneLoading { get { return isLoad; } }
    public bool IsSceneLoadComplete { get { return currentScene == sceneName; } }
    public void LoadScene(Define.SceneName sceneName, bool wait = true)
    {
        isLoad = true;
        this.sceneName = sceneName.ToString();

        Managers.UI.ClearDictionary();
        Managers.UI.ShowUI<SceneLoading_UI>();

        if (!wait)
        {
            Util.GetMonoBehaviour().StartCoroutine(SetSceneLoading());
        }

        loadScene?.Invoke();
    }
    public void ReLoadScene()
    {
        currentScene = "";
        isLoad = true;

        Managers.UI.ClearDictionary();
        Managers.UI.ShowUI<SceneLoading_UI>();

        loadScene?.Invoke();
    }
    public async void SetScene()
    {
        if (!isLoad)
        {
            return;
        }

        await Util.LoadingScene(sceneName.ToString());

        //GC.Collect();

        Managers.UI.IsInitalized = false;
        currentScene = sceneName;
        isLoad = false;
    }
    private IEnumerator SetSceneLoading()
    {
        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
    }
}