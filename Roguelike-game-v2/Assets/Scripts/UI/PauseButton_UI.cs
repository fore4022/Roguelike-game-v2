using UnityEngine;
public class PauseButton_UI : Button_A
{
    protected override void PointerClick()
    {
        Time.timeScale = 0;

        Managers.UI.Show<PauseMenu_UI>();
    }
    protected override void Init()
    {
        maxScale = 1.035f;

        base.Init();
    }
}