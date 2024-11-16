using System;
public class Scene_Manager
{
    public Action loadScene = null;

    private string currentScene;
    private string path = null;

    public string CurrentScene { get { return currentScene; } }
    public void LoadScene(string path)
    {
        this.path = path;

        Managers.UI.ClearDictionary();

        Managers.UI.ShowUI<SceneLoading_UI>();

        loadScene?.Invoke();
    }
    public async void SetScene()
    {
        if(path == null)
        {
            return;
        }
        
        await Util.LoadingScene(path);

        currentScene = path;

        path = null;
    }
}