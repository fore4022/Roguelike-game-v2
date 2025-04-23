using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager
{
    public Action loadScene = null;

    private string sceneName;
    private bool isLoad = false;
    private bool hasInitialization;

    public string CurrentSceneName { get { return sceneName; } }
    public bool IsSceneLoadComplete { get { return !isLoad; } }
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
    public IEnumerator SceneSetting()
    {
        if(!isLoad)
        {
            yield break;
        }

        Util.LoadingScene(sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

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