public class StatUpgradeButton_UI : Button_A
{
    protected override void PointerClick()
    {
        Managers.UI.Get<StatUpgrade_UI>().ToggleUI();
    }
}