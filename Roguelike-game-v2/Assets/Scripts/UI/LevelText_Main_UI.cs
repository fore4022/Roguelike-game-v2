using TMPro;
public class LevelText_Main_UI : UserInterface
{
    private TextMeshProUGUI _levelText;

    public override void SetUserInterface()
    {
        _levelText = GetComponent<TextMeshProUGUI>();

        LevelUpdate();
    }
    public void LevelUpdate()
    {
        if(Managers.UserData.data.Level != UserLevelInfo_SO.maxLevel)
        {
            _levelText.text = $"Lv. {Managers.UserData.data.Level}";
        }
        else
        {
            _levelText.text = "Lv. MAX";
        }
    }
}