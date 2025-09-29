public class StatUpgrade_Button : Button_A
{
    protected override void PointerClick()
    {
        Managers.UI.Get<StatUpgrade_UI>().ToggleUI();
    }
}