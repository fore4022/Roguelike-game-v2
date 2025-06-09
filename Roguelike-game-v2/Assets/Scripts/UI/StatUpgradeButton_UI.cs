public class StatUpgradeButton_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.GetUI<StatUpgrade_UI>().ToggleUI();
    }
}