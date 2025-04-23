using UnityEngine;
public class Resume_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.HideUI<PauseMenu_UI>();
    }
}