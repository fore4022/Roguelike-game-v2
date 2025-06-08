public class StatUpgrade_UI : UserInterface
{
    public FileReference file;

    public override void SetUserInterface()
    {
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
}