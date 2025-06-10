public class StatUpgradeButton_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.Get<StatUpgrade_UI>().ToggleUI();
    }
}