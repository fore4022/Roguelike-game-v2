using UnityEngine;
public class Resume_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.ShowUI<HeadUpDisplay_UI>();
        Managers.UI.HideUI<PauseMenu_UI>();
    }
}