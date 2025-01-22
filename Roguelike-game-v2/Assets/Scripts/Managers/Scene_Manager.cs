using System;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene;
    private string sceneName = null;
    private bool isLoad = false;
    
    public bool IsSceneLoadComplete { get { return currentScene == sceneName; } }
    public void LoadScene(string sceneName)
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

        await Util.LoadingScene(sceneName);

        GC.Collect();

        Managers.UI.isInitalized = false;
        currentScene = sceneName;
        isLoad = false;
    }
}