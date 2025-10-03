using TMPro;
public class LevelText_UI : UserInterface
{
    private TextMeshProUGUI _levelText;

    public override void SetUserInterface()
    {
        _levelText = GetComponent<TextMeshProUGUI>();

        Managers.Game.inGameData_Manage.player.levelUpdate += LevelUpdate;
    }
    public void LevelUpdate()
    {
        _levelText.text = $"Lv. {Managers.Game.inGameData_Manage.player.Level}";
    }
}