public class PauseMenu_UI : UserInterface
{
    protected override void Start()
    {
        base.Start();

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}