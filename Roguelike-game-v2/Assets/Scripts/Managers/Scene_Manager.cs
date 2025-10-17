using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager
{
    public Action loadScene = null;
    public Action loadComplete = null;

    private string sceneName = "Title";
    private bool isLoading = false;
    private bool hasInitialization;

    public string CurrentSceneName { get { return sceneName; } }
    public bool IsLoading { get { return !isLoading; } }
    public void LoadScene(SceneName sceneName, bool hasInitialization = true)
    {
        isLoading = true;
        this.hasInitialization = hasInitialization;
        this.sceneName = sceneName.ToString();

        Managers.UI.ClearDictionary();
        Managers.UI.Show<SceneLoading_UI>();

        if(!hasInitialization)
        {
            Util.GetMonoBehaviour().StartCoroutine(SetSceneLoading());
        }

        loadScene?.Invoke();
    }
    public IEnumerator SceneSetting()
    {
        if(!isLoading)
        {
            yield break;
        }

        Addressable_Helper.LoadingScene(sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        loadComplete?.Invoke();

        isLoading = false;

        if(!hasInitialization)
        {
            Managers.UI.Get<SceneLoading_UI>().Wait = false;
        }
    }
    private IEnumerator SetSceneLoading()
    {
        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() != null);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;
    }
}