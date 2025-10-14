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
        if(Managers.Data.data.Level != UserLevelData_SO.maxLevel)
        {
            expSlider.value = (float)Managers.Data.data.Exp / Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.data.Level - 1];
            expText.text = $"{Managers.Data.data.Exp:N0} / {Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.data.Level - 1]:N0}";
        }
        else
        {
            expText.text = $"{Managers.Data.data.Exp:N0}";
        }
    }
}