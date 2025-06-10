public class SettingButton_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.Show<Setting_UI>();
    }
}