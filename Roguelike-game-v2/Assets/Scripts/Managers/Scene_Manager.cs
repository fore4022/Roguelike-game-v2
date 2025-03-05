using System;
public class Scene_Manager
{
    public Action loadScene = null;

    private Define.SceneName currentScene;
    private Define.SceneName sceneName;
    private bool isLoad = false;
    
    public bool IsSceneLoadComplete { get { return currentScene == sceneName; } }
    public void LoadScene(Define.SceneName sceneName)
    {
        isLoad = true;
        this.sceneName = sceneName;

        Managers.UI.ClearDictionary();
        Managers.UI.ShowUI<SceneLoading_UI>();
        loadScene?.Invoke();
    }
    public async void SetScene()
    {
        if(!isLoad)
        {
            return;
        }

        await Util.LoadingScene(sceneName.ToString());

        GC.Collect();

        Managers.UI.isInitalized = false;
        currentScene = sceneName;
        isLoad = false;
    }
}