using TMPro;
public class Level_Main_UI : UserInterface
{
    private TextMeshProUGUI levelText;

    public override void SetUserInterface()
    {
        levelText = GetComponent<TextMeshProUGUI>();

        Init();
    }
    private void Init()
    {
        levelText.text = $"Lv. {Managers.UserData.data.Level}";
    }
}