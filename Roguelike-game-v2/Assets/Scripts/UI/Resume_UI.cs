using UnityEngine;
public class Resume_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Hide<PauseMenu_UI>();
    }
}