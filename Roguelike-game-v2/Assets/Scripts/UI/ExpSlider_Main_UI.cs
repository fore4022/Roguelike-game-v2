using TMPro;
using UnityEngine.UI;
public class ExpSlider_Main_UI : UserInterface
{
    private Slider expSlider;
    private TextMeshProUGUI expText;

    public override void SetUserInterface()
    {
        expSlider = GetComponent<Slider>();
        expText = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        UpdateExp();
    }
    public void UpdateExp()
    {
        if(Managers.UserData.data.Level != UserLevelInfo_SO.maxLevel)
        {
            expSlider.value = (float)Managers.UserData.data.Exp / Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
            expText.text = $"{Managers.UserData.data.Exp:N0} / {Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1]:N0}";
        }
        else
        {
            expText.text = $"{Managers.UserData.data.Exp:N0}";
        }
    }
}