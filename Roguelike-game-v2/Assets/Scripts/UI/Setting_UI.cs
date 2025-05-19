public class Setting_UI : UserInterface
{
    public override void SetUserInterface()
    {
        Managers.UI.HideUI<Setting_UI>();
    }
    public void Confirm()
    {
        Managers.UI.GetUI<PauseMenu_UI>().ShowIcons();
    }
}