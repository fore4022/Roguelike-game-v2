public class PauseMenu_UI : UserInterface
{
    protected override void Awake()
    {
        base.Awake();

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}