using UnityEngine;
public class NextStageButton_UI : Button_2
{
    [SerializeField]
    private int sign;

    protected override void PointerClick()
    {
        Managers.UI.GetUI<StageIcon_UI>().UpdateUI(sign);
    }
}