public class StatUpgrade_UI : UserInterface
{
    public FileReference file;

    public override void SetUserInterface()
    {
        //Set

        //Managers.UI.HideUI<StatUpgrade_UI>();
    }
    protected override void Enable()
    {
        transform.SetPosition(new(0, 35), 1);
    }
}