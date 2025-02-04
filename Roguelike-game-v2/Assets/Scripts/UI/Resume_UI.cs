using UnityEngine;
public class Resume_UI : NewButton
{
    public override void PointerClick()
    {
        Time.timeScale = 1f;

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}