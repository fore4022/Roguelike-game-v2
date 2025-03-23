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
        levelText.text = $"Lv. {Managers.UserData.data.Level}";
    }
}