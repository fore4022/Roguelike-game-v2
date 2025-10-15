using TMPro;
using UnityEngine.UI;
public class UserExpSlider_UI : UserInterface
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
        if(Managers.Data.user.Level != UserLevelData_SO.maxLevel)
        {
            expSlider.value = (float)Managers.Data.user.Exp / Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.user.Level - 1];
            expText.text = $"{Managers.Data.user.Exp:N0} / {Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.user.Level - 1]:N0}";
        }
        else
        {
            expText.text = $"{Managers.Data.user.Exp:N0}";
        }
    }
}