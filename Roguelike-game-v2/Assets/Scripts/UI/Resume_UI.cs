using UnityEngine;
public class Resume_UI : NewButton
{
    protected override void PointerClick()
    {
        Time.timeScale = 1f;

        Managers.UI.ShowUI<HUD_UI>();
        Managers.UI.HideUI<PauseMenu_UI>();
    }
    protected override void Init()
    {
        minScale = 1f;
        maxScale = 1.075f;
        minAlpha = 205;
        maxAlpha = 255;
        duration = 0.1f;
    }
}