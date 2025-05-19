public class SettingButton_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.GetUI<PauseMenu_UI>().HideIcons();
        Managers.UI.ShowUI<Setting_UI>();
    }
}