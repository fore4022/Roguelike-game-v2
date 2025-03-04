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

        Init();
    }
    private void Init()
    {
        expSlider.value = Managers.UserData.data.UserExperience / Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.UserLevel - 1];
        expText.text = $"{Managers.UserData.data.UserExperience} / {Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.UserLevel - 1]}";
    }
}