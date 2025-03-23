using TMPro;
public class Level_Main_UI : UserInterface
{
    private TextMeshProUGUI levelText;

    public override void SetUserInterface()
    {
        levelText = GetComponent<TextMeshProUGUI>();

        UpdateLevel();
    }
    public void UpdateLevel()
    {
        if(Managers.UserData.data.Level != UserLevelInfo_SO.maxLevel)
        {
            levelText.text = $"Lv. {Managers.UserData.data.Level}";
        }
        else
        {
            levelText.text = "Lv. MAX";
        }
    }
}