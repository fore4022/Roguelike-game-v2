public class SettingButton_UI : Button_A
{
    protected override void PointerClick()
    {
        Managers.UI.Show<Setting_UI>();
    }
}