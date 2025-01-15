using UnityEngine;
public class Resume_UI : NewButton
{
    protected override void PointerClick()
    {
        Time.timeScale = 1f;

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}