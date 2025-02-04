using UnityEngine;
public class Resume_UI : Button_1
{
    public override void PointerClick()
    {
        Time.timeScale = 1f;

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}