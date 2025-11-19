public class Setting_Button : Button_A
{
    protected override void PointerClick()
    {
        Managers.UI.Show<Setting_UI>();
    }
}