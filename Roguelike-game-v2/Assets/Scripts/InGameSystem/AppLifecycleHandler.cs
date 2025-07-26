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
            if(!Managers.Game.GameOver)
            {
                if(focus)
                {
                    Managers.UserData.Save();
                }
                else
                {
                    if(Managers.Game.Playing)
                    {
                        Time.timeScale = 0;

                        Managers.UI.Show<PauseMenu_UI>();
                    }
                }
            }
        }
    }
}