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
        expSlider.value = Managers.UserData.data.Exp / Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
        expText.text = $"{Managers.UserData.data.Exp} / {Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1]}";
    }
}