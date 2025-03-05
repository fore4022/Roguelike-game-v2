using System;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene = null;
    private string sceneName;
    private bool isLoad = false;
    
    public bool IsSceneLoadComplete { get { return currentScene == sceneName; } }
    public void LoadScene(Define.SceneName sceneName)
    {
        isLoad = true;
        this.sceneName = sceneName.ToString();

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

        Managers.UI.IsInitalized = false;
        currentScene = sceneName;
        isLoad = false;
    }
}