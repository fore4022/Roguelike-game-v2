using TMPro;
public class UserLevelText_UI : UserInterface
{
    private TextMeshProUGUI _levelText;

    public override void SetUserInterface()
    {
        _levelText = GetComponent<TextMeshProUGUI>();

        LevelUpdate();
    }
    public void LevelUpdate()
    {
        if(Managers.Data.data.Level != UserLevelData_SO.maxLevel)
        {
            _levelText.text = $"Lv. {Managers.Data.data.Level}";
        }
        else
        {
            _levelText.text = "Lv. MAX";
        }
    }
}