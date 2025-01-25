using UnityEngine;
public class NextStageButton_UI : NewButton
{
    [SerializeField]
    private int sign;

    protected override void PointerClick()
    {
        Managers.UI.GetUI<StageIcon_UI>().UpdateUI();
    }
}