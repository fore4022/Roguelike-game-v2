public class StatUpgrade_UI : UserInterface
{
    public override void SetUserInterface()
    {
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
    public void IncreaseAAA()
    {
        
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
}