using UnityEngine;
public class AppLifecycleHandler : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Managers.UserData.Save();
    }
    private void OnApplicationFocus(bool focus)
    {
        if(Managers.Scene.CurrentSceneName.Equals(SceneName.InGame.ToString()))
        {
            if(!Managers.Game.IsGameOver)
            {
                if(focus)
                {
                    Managers.UserData.Save();
                }
                else
                {
                    if(Managers.Game.IsPlaying)
                    {
                        Time.timeScale = 0;

                        Managers.UI.Show<PauseMenu_UI>();
                    }
                }
            }
        }
    }
}