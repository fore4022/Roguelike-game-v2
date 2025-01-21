using System;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene;
    private string sceneName = null;

    public string CurrentScene { get { return currentScene; } }
    public string SceneName { get { return sceneName; } }
    public void LoadScene(string path)
    {
        this.sceneName = path;

        Managers.UI.ClearDictionary();
        Managers.UI.ShowUI<SceneLoading_UI>();
        loadScene?.Invoke();
    }
    public async void SetScene()
    {
        if(sceneName == null)
        {
            return;
        }

        await Util.LoadingScene(sceneName);

        GC.Collect();

        Managers.UI.isInitalized = false;
        currentScene = sceneName;
        sceneName = null;
    }
}