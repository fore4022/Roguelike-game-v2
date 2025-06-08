public class StatUpgradeButton_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.UI.ShowUI<StatUpgrade_UI>();
    }
}