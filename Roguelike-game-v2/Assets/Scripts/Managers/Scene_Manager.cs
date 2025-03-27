using System;
using System.Collections;
using UnityEngine;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene = null;
    private string sceneName;
    private bool isLoad = false;
    private bool hasInitialization;

    public bool IsSceneLoadComplete { get { return currentScene == sceneName; } }
    public void LoadScene(Define.SceneName sceneName, bool hasInitialization = true)
    {
        isLoad = true;
        this.hasInitialization = hasInitialization;
        this.sceneName = sceneName.ToString();

        Managers.UI.ClearDictionary();
        Managers.UI.ShowUI<SceneLoading_UI>();

        if(!hasInitialization)
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
    public void SetScene()
    {
        if(!isLoad)
        {
            return;
        }

        Util.LoadingScene(sceneName.ToString());

        currentScene = sceneName;
        isLoad = false;

        if(!hasInitialization)
        {
            Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
        }
    }
    private IEnumerator SetSceneLoading()
    {
        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
    }
}