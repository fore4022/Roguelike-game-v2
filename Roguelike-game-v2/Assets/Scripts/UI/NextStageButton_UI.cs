using UnityEngine;
public class NextStageButton_UI : NewButton
{
    [SerializeField]
    private int sign;

    public override void PointerClick()
    {
        Managers.UI.GetUI<StageIcon_UI>().UpdateUI(sign);
    }
}