using UnityEngine;
public class Pause_UI : Button_1
{
    public override void PointerClick()
    {
        Time.timeScale = 0;

        Managers.UI.ShowUI<PauseMenu_UI>();
    }
    protected override void Init()
    {
        maxScale = 1.035f;
    }
}