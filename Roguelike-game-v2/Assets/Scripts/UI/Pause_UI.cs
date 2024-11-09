using UnityEngine;
public class Pause_UI : NewButton
{
    protected override void Awake()
    {
        base.Awake();

        Managers.UI.HideUI<Pause_UI>();
    }
    protected override void PointerClick()
    {
        Time.timeScale = 0;

        Managers.UI.ShowUI<PauseMenu_UI>();
        Managers.UI.HideUI<HUD_UI>();
    }
    protected override void Init()
    {
        minScale = 1f;
        maxScale = 1.035f;
        minAlpha = 155;
        maxAlpha = 235;
        duration = 0.1f;
    }
}